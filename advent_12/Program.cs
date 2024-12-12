namespace advent_12;

class Program
{
    public static List<List<char>> garden = File.ReadAllLines("input.txt").Select(x => x.ToCharArray().ToList()).ToList();
    static void Main(string[] args)
    {
        int part1 = 0;
        int part2 = 0;
        List<List<Tuple<char, int, int, int>>> regions = [];
        //             <char, y,   x,   neighboring>

        for (int y = 0; y < garden.Count; y++)
        {
            for (int x = 0; x < garden[y].Count; x++)
            {
                char current = garden[y][x];
                if (!regions.Any(i => i.Any(j => j.Item2 == y && j.Item3 == x)))
                {
                    regions.Add(around(current, y, x, []).Distinct().ToList()); // i tried my best but since i did depth-first, duplicates can occur
                }
            }
        }

        regions.ForEach(region =>
        {

            int sides = 0;
            int border = region.Select(p => Convert.ToString(p.Item4, 2).PadLeft(4, '0').Where(x => x == '1').Count()).Sum();

            // groupby y then check for top edge. then group by x increasing by 1 
            int top = region.GroupBy(plant => plant.Item2)
                .Select(g => g
                    .Where(p => Convert.ToString(p.Item4, 2).PadLeft(4, '0')[0] == '1')
                    .OrderBy(p => p.Item3)
                )
                .Where(g => g.Count() > 0)
                .Select(g => g
                    .GroupWhile((p, n) => p.Item3 + 1 == n.Item3)
                    .Count()
                ).Sum();

            // groupby y then check for bottom edge. then group by x increasing by 1 
            int bottom = region.GroupBy(plant => plant.Item2)
                .Select(g => g
                    .Where(p => Convert.ToString(p.Item4, 2).PadLeft(4, '0')[1] == '1')
                    .OrderBy(p => p.Item3)
                )
                .Where(g => g.Count() > 0)
                .Select(g => g
                    .GroupWhile((p, n) => p.Item3 + 1 == n.Item3)
                    .Count()
                ).Sum();

            // groupby x then check for left edge. then group by y increasing by 1 
            int left = region.GroupBy(plant => plant.Item3)
                .Select(g => g
                    .Where(p => Convert.ToString(p.Item4, 2).PadLeft(4, '0')[2] == '1')
                    .OrderBy(p => p.Item2)
                )
                .Where(g => g.Count() > 0)
                .Select(g => g
                    .GroupWhile((p, n) => p.Item2 + 1 == n.Item2)
                    .Count()
                ).Sum();

            // groupby x then check for right edge. then group by y increasing by 1 
            int right = region.GroupBy(plant => plant.Item3)
                .Select(g => g
                    .Where(p => Convert.ToString(p.Item4, 2).PadLeft(4, '0')[3] == '1')
                    .OrderBy(p => p.Item2)
                )
                .Where(g => g.Count() > 0)
                .Select(g => g
                    .GroupWhile((p, n) => p.Item2 + 1 == n.Item2)
                .Count()
                ).Sum();

            sides = top + bottom + left + right;
            Console.WriteLine("plant: " + region[0].Item1 + " area: " + region.Count() + " sides: " + sides);
            part1 += border * region.Count();
            part2 += sides * region.Count();
        });

        Console.WriteLine("Part 1: " + part1);
        Console.WriteLine("Part 2: " + part2);
    }

    static List<Tuple<char, int, int, int>> around(char plant, int y, int x, List<Tuple<char, int, int, int>> region)
    {
        List<Tuple<char, int, int, int>> newplants = [];
        List<Tuple<char, int, int>> testfor = [];
        int sides = 15; // binary - each bit is whether theres a side or not. T B L R

        if (y > 0 && garden[y - 1][x] == plant)
        {
            Tuple<char, int, int> test = new(plant, y - 1, x);

            if (region.Any(x => x.Item1 == test.Item1 && x.Item2 == test.Item2 && x.Item3 == test.Item3))
            { testfor.Add(test); }

            sides -= 8;
        }

        if (y < garden.Count - 1 && garden[y + 1][x] == plant)
        {
            Tuple<char, int, int> test = new(plant, y + 1, x);

            if (region.Any(x => x.Item1 == test.Item1 && x.Item2 == test.Item2 && x.Item3 == test.Item3))
            { testfor.Add(test); }

            sides -= 4;
        }

        if (x > 0 && garden[y][x - 1] == plant)
        {
            Tuple<char, int, int> test = new(plant, y, x - 1);

            if (region.Any(x => x.Item1 == test.Item1 && x.Item2 == test.Item2 && x.Item3 == test.Item3))
            { testfor.Add(test); }

            sides -= 2;
        }

        if (x < garden[y].Count - 1 && garden[y][x + 1] == plant)
        {
            Tuple<char, int, int> test = new(plant, y, x + 1);

            if (region.Any(x => x.Item1 == test.Item1 && x.Item2 == test.Item2 && x.Item3 == test.Item3))
            { testfor.Add(test); }

            sides--;
        }

        newplants.Add(new(plant, y, x, sides));
        region.AddRange(newplants);

        testfor.ForEach(plant =>
        {
            newplants.AddRange(around(plant.Item1, plant.Item2, plant.Item3, region));
        });

        return newplants;
    }
}

public static class Extensions
{
    public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this IEnumerable<T> seq, Func<T, T, bool> condition)
    {
        T prev = seq.First();
        List<T> list = new List<T>() { prev };

        foreach (T item in seq.Skip(1))
        {
            if (condition(prev, item) == false)
            {
                yield return list;
                list = new List<T>();
            }
            list.Add(item);
            prev = item;
        }

        yield return list;
    }
}
