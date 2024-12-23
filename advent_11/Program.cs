namespace advent_11;

class Program
{
    public static Dictionary<Tuple<long, long>, long> aa = [];

    static long next(long stone, long step, long max)
    {
        if (step == max) { return 1; }

        Tuple<long, long> key = new(stone, step);

        long value;
        if (aa.TryGetValue(key, out value)) { return value; }

        if (stone == 0)
        {
            value = next(1, step + 1, max);
            aa.Add(key, value);
            return value;
        }

        if (stone.ToString().Length % 2 == 0)
        {
            long first = long.Parse(stone.ToString().Substring(0, stone.ToString().Length / 2));
            long second = long.Parse(stone.ToString().Substring(stone.ToString().Length / 2));

            long a = next(first, step + 1, max);
            long b = next(second, step + 1, max);
            aa.Add(key, a + b);
            return a + b;
        }

        value = next(stone * 2024, step + 1, max);
        aa.Add(key, value);

        return value;
    }

    static public void Main(string[] args)
    {
        Console.WriteLine(File.ReadAllText("input.txt").Split(" ").Select(long.Parse).Select(x => next(x, 0, 75)).Sum());

        /* the below is archived for funny moments. im stupid.


        List<long> lstones = File.ReadAllText("input.txt").Split(" ").Select(long.Parse).ToList();
        List<string> preprocess = File.ReadAllText("preprocess/input_25_distinct").Split(" ").ToList();

        Parallel.ForEach(preprocess, read =>
        {
            TextWriter dtw = new StreamWriter($"preprocess/{read}_50");

            foreach (string stone in File.ReadAllText("preprocess/" + read + "_25").Trim().Split(" "))
            {
                dtw.Write(File.ReadAllText("preprocess/" + stone + "_25").Trim() + " ");
            }

            dtw.Flush();
            dtw.Close();
            Console.WriteLine("i love you anya");
        });


        foreach (long stone in preprocess)
        {
            List<long> stones = [stone];
            for (long b = 0; b < 25; b++)
            {
                for (int s = 0; s < stones.Count; s++)
                {
                    if (stones[s] == 0) { stones[s] = 1; }

                    else if (stones[s].ToString().Length % 2 == 0)
                    {
                        string first = stones[s].ToString().Substring(0, stones[s].ToString().Length / 2);
                        string second = stones[s].ToString().Substring(stones[s].ToString().Length / 2);

                        stones[s] = long.Parse(first);
                        s++;
                        stones.Insert(s, long.Parse(second));
                    }

                    else { stones[s] *= 2024; }
                }

                TextWriter dtw = new StreamWriter($"preprocess/{stone}_25");
                foreach (long hstone in stones) { dtw.Write(" " + hstone.ToString()); }
                dtw.Flush();
                dtw.Close();
            }
        }


        my plan here was to "preprocess" numbers. calculate what 0 becomes after 25 blinks, then 1 and just replace
        i started with fucking differentiation and integration
        BUT. if you add a single char between all of these:

        25 blinks for zero produces 65536 characters (equal to 2^16)
        20 blinks for zero produces 4096 characters (equal to 2^12)

        25 blinks for one produces 106496 characters
        equal to 2^13 * 13

        25 blinks for 2,3,4 produces 102400
        equal to 2^12 * 15

        25 blinks for four produces

        25 blinks for seventeen produces 131072 characters
        equal to 2^17

        25 blinks for 125 doesn't have remarkable factors

        ^^^^^^ okay so all of the above was BULLSHIT ^^^^^^
        i forgot to flush and close my streamwriter, so it was only writing every 2^n characters. FUCK

        there seems to be 54 distinct numbers for a lot of the above values, but 379 when i run the input data


        TextWriter tw = new StreamWriter("input_25");
        foreach (long stone in stones) { tw.Write(stone.ToString() + " "); }
        tw.Flush();
        tw.Close();
        stones = stones.Distinct().Order().ToList();
        Console.WriteLine("Part 1: " + stones.Distinct().Count());

        TextWriter dtw = new StreamWriter("input_25_distinct");
        foreach (long stone in stones) { dtw.Write(stone.ToString() + " "); }
        dtw.Flush();
        dtw.Close();
        */
    }
}