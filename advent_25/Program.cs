namespace advent_25;

class Program
{
    static void Main(string[] args)
    {
        var locksandkeys = File.ReadAllText("input.txt").Split("\n\n").Select(x => x.Split("\n")).ToList();

        List<List<int>> keys = [];
        List<List<int>> locks = [];

        locksandkeys.ForEach(lok =>
        {
            if (lok[0] == "#####") // lock
            {
                List<int> n = [0, 0, 0, 0, 0];
                for (int i = 1; i < 6; i++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (lok[i][x] == '#') { n[x]++; }
                    }
                }
                locks.Add(n);
            }

            else // key
            {
                List<int> n = [0, 0, 0, 0, 0];
                for (int i = 1; i < 6; i++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (lok[i][x] == '#') { n[x]++; }
                    }
                }
                keys.Add(n);
            }
        });

        int part1 = 0;

        locks.ForEach(l =>
        {
            for (int k = 0; k < keys.Count; k++)
            {
                bool match = true;
                for (int i = 0; i < 5; i++)
                {
                    if (l[i] + keys[k][i] > 5) { match = false; break; }
                }

                if (match) { part1++; }
            }
        });

        Console.WriteLine("Part 1: " + part1);
    }
}
