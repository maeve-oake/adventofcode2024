namespace advent_22;

class Program
{
    static void Main(string[] args)
    {
        List<long> input = File.ReadAllLines("input.txt").Select(long.Parse).ToList();

        long part1 = 0;

        input.ForEach(x =>
        {
            long h = x;
            for (int i = 0; i < 2000; i++)
            {
                h = NextSecret(h);
            }
            part1 += h;
            //Console.WriteLine($"{x}: {h}");
        });

        Console.WriteLine("Part 1: " + part1);

        long part2 = 0;

        List<List<long>> values = [];
        input.ForEach(x =>
        {
            values.Add([]);
            for (int i = 0; i < 2000; i++)
            {
                values.Last().Add(x % 10);
                x = NextSecret(x);
            }
        });

        List<Dictionary<string, int>> diffs = [];
        values.ForEach(buyer =>
        {
            diffs.Add([]);
            for (int i = 4; i < buyer.Count; i++)
            {
                string h = $"{buyer[i - 3] - buyer[i - 4]},{buyer[i - 2] - buyer[i - 3]},{buyer[i - 1] - buyer[i - 2]},{buyer[i] - buyer[i - 1]}";
                if (!diffs.Last().TryGetValue(h, out _)) { diffs.Last()[h] = (int)buyer[i] % 10; };
            }
        });

        List<string> sorts = [];
        diffs.ForEach(x =>
        {
            x.ToList().ForEach(l =>
            {
                sorts.Add(l.Key);
            });
        });

        sorts.Distinct().ToList().ForEach(x =>
        {
            int i = 0;
            diffs.ForEach(buyer =>
            {
                int h;
                if (buyer.TryGetValue(x, out h)) { i += h; }
            });

            part2 = i > part2 ? i : part2;
        });

        Console.WriteLine("Part 2: " + part2);
    }

    static long NextSecret(long num)
    {
        num ^= num * 64;
        num %= 16777216;
        num = (num ^ (num / 32)) % 16777216;
        num ^= num * 2048;
        num %= 16777216;

        return num;
    }
}
