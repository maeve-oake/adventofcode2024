namespace advent_7;

class Program
{
    static void Main(string[] args)
    {
        List<KeyValuePair<long, List<long>>> input = File.ReadAllLines("input.txt").Select(x => { string[] p = x.Split(": "); return new KeyValuePair<long, List<long>>(long.Parse(p[0]), p[1].Split(" ").Select(long.Parse).ToList()); }).ToList();

        Console.WriteLine("Part 1: " + input.Where(CalculateP1).Select(x => x.Key).Sum());
        Console.WriteLine("Part 2: " + input.Where(CalculateP2).Select(x => x.Key).Sum());
    }

    static bool CalculateP1(KeyValuePair<long, List<long>> input)
    {
        bool possible = false;
        for (long i = 0; i < Math.Pow(2, input.Value.Count() - 1); i++)
        {
            long total = input.Value[0];

            for (int j = 0; j < input.Value.Count() - 1; j++)
            {
                if ((i & (1 << j)) > 0) { total += input.Value[j + 1]; }
                else { total *= input.Value[j + 1]; }
            }

            if (total == input.Key) { possible = true; break; }
        }

        return possible;
    }

    static bool CalculateP2(KeyValuePair<long, List<long>> input)
    {
        bool possible = false;
        for (long i = 0; i < Math.Pow(3, input.Value.Count() - 1); i++)
        {
            long total = input.Value[0];
            string qbits = IntToString(i, ['0', '1', '2']);

            for (int j = 0; j < input.Value.Count() - 1; j++)
            {
                char qbit = qbits.PadLeft(input.Value.Count() - 1, '0')[j];

                if (qbit == '0')
                { total += input.Value[j + 1]; }
                else if (qbit == '1')
                { total *= input.Value[j + 1]; }
                else if (qbit == '2')
                { total = long.Parse(total.ToString() + input.Value[j + 1]); }
            }

            if (total == input.Key) { possible = true; break; }
        }

        return possible;
    }

    public static string IntToString(long value, char[] baseChars)
    {
        string result = string.Empty;
        int targetBase = baseChars.Length;

        do
        {
            result = baseChars[value % targetBase] + result;
            value = value / targetBase;
        }
        while (value > 0);

        return result;
    }
}
