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
        int l = lines[0].Length;
        int l2 = lines[0].Length - 1;
        string wordR = new string(word.Reverse().ToArray());

        // vertically
        count += Regex.Matches(input, $"(?=(X.{{{l}}}M.{{{l}}}A.{{{l}}}S|S.{{{l}}}A.{{{l}}}M.{{{l}}}X))", RegexOptions.Singleline).Count();

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
        Console.WriteLine("Part 2: " + Regex.Matches(input, $"(?=(M.S.{{{l2}}}A.{{{l2}}}M.S|M.M.{{{l2}}}A.{{{l2}}}S.S|S.S.{{{l2}}}A.{{{l2}}}M.M|S.M.{{{l2}}}A.{{{l2}}}S.M))", RegexOptions.Singleline).Count());
    }
}