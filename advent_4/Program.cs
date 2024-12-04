namespace advent_4;

using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string input = File.ReadAllText("input.txt");
        List<string> lines = input.Split("\n").ToList();
        int count = 0;
        string word = "XMAS";
        int p2skip = lines[0].Length - 1;
        string wordR = new string(word.Reverse().ToArray());

        // vertically

        for (int x = 0; x < lines[0].Length; x++)
        {
            string line = "";
            for (int y = 0; y < lines.Count; y++)
            {
                line += lines[y][x];

            }
            count += Regex.Matches(line, $"(?={word}|{wordR})").Count();
        }

        // horizontally
        count += Regex.Matches(input, $"(?={word}|{wordR})").Count();

        // diagonally 
        for (int y = 0; y < lines.Count - word.Length + 1; y++)
        {
            for (int x = 0; x < lines[0].Length - word.Length + 1; x++)
            {
                string cur = "";
                for (int c = 0; c < word.Length; c++) { cur += lines[y + c][x + c]; }

                if (cur == word || cur == wordR) { count++; }
            }
        }
        for (int y = 0; y < lines.Count - word.Length + 1; y++)
        {
            for (int x = lines[0].Length - 1; x > word.Length - 2; x--)
            {
                string cur = "";
                for (int c = 0; c < word.Length; c++) { cur += lines[y + c][x - c]; }

                if (cur == word || cur == wordR) { count++; }
            }
        }

        Console.WriteLine("Part 1: " + count);

        // part 2
        Console.WriteLine("Part 2: " + Regex.Matches(input, $"(?=(M.S.{{{p2skip}}}A.{{{p2skip}}}M.S|M.M.{{{p2skip}}}A.{{{p2skip}}}S.S|S.S.{{{p2skip}}}A.{{{p2skip}}}M.M|S.M.{{{p2skip}}}A.{{{p2skip}}}S.M))", RegexOptions.Singleline).Count());
    }
}