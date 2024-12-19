namespace advent_19;

class Program
{
    static string[] input = File.ReadAllText("input.txt").Split("\n\n");
    static List<string> patterns = input[0].Split(", ").ToList();

    public static Dictionary<string, long> fuckinTOWELS = [];

    static void Main(string[] args)
    {
        int part1 = 0;
        long part2 = 0;

        List<string> towels = input[1].Split("\n").ToList();

        towels.ForEach(towel =>
        {
            part1 += makeTheFuckingTowel(towel) ? 1 : 0;
        });

        Console.WriteLine("Part 1: " + part1);

        towels.ForEach(towel =>
        {
            part2 += MOREMOUSEBITES(towel);
        });

        Console.WriteLine("Part 2: " + part2);
    }

    static bool makeTheFuckingTowel(string towel)
    {
        if (towel.Length == 0) { return true; };

        foreach (string pattern in patterns)
        {
            if (towel.StartsWith(pattern))
            {
                if (makeTheFuckingTowel(towel.Substring(pattern.Length)))
                {
                    return true;
                };
            }
        };

        return false;
    }

    static long MOREMOUSEBITES(string towel)
    {
        if (towel.Length == 0) { return 1; }

        long value;
        if (fuckinTOWELS.TryGetValue(towel, out value)) { return value; }

        foreach (string pattern in patterns)
        {
            if (towel.StartsWith(pattern))
            {
                value += MOREMOUSEBITES(towel.Substring(pattern.Length));
            }
        };

        fuckinTOWELS.Add(towel, value);

        return value;
    }
}
