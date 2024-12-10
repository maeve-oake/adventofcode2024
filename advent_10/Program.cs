namespace advent_10;

class Program
{
    static void Main(string[] args)
    {
        List<List<int>> input = File.ReadAllLines("input.txt").Select(x => x.ToCharArray().Select(x => x - 48).ToList()).ToList();
        int p1total = 0;
        int p2total = 0;

        // gonna have to do some kind of fuckass recursion... it's only 10 steps so a for loop would probably work anyway
        // mmm depth first search lfg  

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Count; x++)
            {
                if (input[y][x] == 0)
                {
                    Console.WriteLine("\nStarting search: " + y + "," + x);
                    List<string> temp = fuckingGoForIt(input, y, x, new List<string>());
                    p1total += temp.Distinct().Count();
                    p2total += temp.Count();
                }
            }
        }

        Console.WriteLine("\nPart 1: " + p1total);
        Console.WriteLine("Part 2: " + p2total);
    }

    public static List<string> fuckingGoForIt(List<List<int>> input, int y, int x, List<string> nines)
    {
        int looking = input[y][x] + 1;
        List<int> ny = [];
        List<int> nx = [];

        if (y > 0 && input[y - 1][x] == looking)
        {
            if (looking == 9) { nines.Add(y - 1 + "," + x); }
            else { ny.Add(y - 1); nx.Add(x); }
        }

        if (y < input.Count - 1 && input[y + 1][x] == looking)
        {
            if (looking == 9) { nines.Add(y + 1 + "," + x); }
            else { ny.Add(y + 1); nx.Add(x); }
        }

        if (x > 0 && input[y][x - 1] == looking)
        {
            if (looking == 9) { nines.Add(y + "," + (x - 1)); }
            else { ny.Add(y); nx.Add(x - 1); }
        }

        if (x < input[y].Count - 1 && input[y][x + 1] == looking)
        {
            if (looking == 9) { nines.Add(y + "," + (x + 1)); }
            else { ny.Add(y); nx.Add(x + 1); }
        }

        for (int i = 0; i < ny.Count; i++)
        {
            Console.WriteLine("Next: " + looking + " at " + ny[i] + "," + nx[i]);
            nines = fuckingGoForIt(input, ny[i], nx[i], nines);
        }

        Console.WriteLine("end of branch! C = " + nines.Count());
        return nines;
    }
}
