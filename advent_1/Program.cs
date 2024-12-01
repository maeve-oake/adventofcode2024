namespace advent_1;

class Program
{
    static void Main(string[] args)
    {
        List<string> lines = File.ReadAllLines("input.txt").ToList();

        List<int> left = [];
        List<int> right = [];
        int part1 = 0;
        int part2 = 0;

        lines.ForEach(line =>
        {
            left.Add(int.Parse(line.Split("   ")[0]));
            right.Add(int.Parse(line.Split("   ")[1]));
        });

        left.Sort();
        right.Sort();

        for (int i = 0; i < left.Count(); i++)
        {
            part1 += Math.Abs(left[i] - right[i]); // part 1 
            part2 += left[i] * right.Where(x => x.Equals(left[i])).Count(); // part 2
        }

        Console.WriteLine("Part 1: " + part1);
        Console.WriteLine("Part 2: " + part2);
    }
}
