namespace advent_5;

class Program
{
    static void Main(string[] args)
    {
        List<List<int>> rules = File.ReadAllText("test.txt").Split("\n\n")[0].Split("\n").Select(x => x.Split("|").Select(int.Parse).ToList()).ToList();
        List<List<int>> updates = File.ReadAllText("test.txt").Split("\n\n")[1].Split("\n").Select(x => x.Split(",").Select(int.Parse).ToList()).ToList();

        int part1 = 0;
        int part2 = 0;

        updates.ForEach(update =>
        {
            bool allowed = true;

            for (int page = 0; page < update.Count && allowed; page++)
            {
                rules.Where(x => x[0] == update[page]).ToList().ForEach(rule =>
                {
                    int found = update.FindIndex(x => x == rule[1]);
                    if (found < page && found >= 0) { allowed = false; }
                });
            }

            if (allowed) { part1 += update[update.Count / 2]; }
        });

        Console.WriteLine("Part 1: " + part1);
        Console.WriteLine("Part 2: " + part2);
    }
}
