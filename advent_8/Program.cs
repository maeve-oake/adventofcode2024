namespace advent_8;

class Program
{
    static void Main(string[] args)
    {
        List<List<char>> grid = File.ReadAllLines("input.txt").Select(x => x.ToList()).ToList();
        List<string> antinodes = [];
        List<string> p2antinodes = [];

        for (int y = 0; y < grid.Count(); y++)
        {
            for (int x = 0; x < grid[y].Count(); x++)
            {
                if (grid[y][x] != '.')
                {
                    char curFreq = grid[y][x];

                    for (int y2 = 0; y2 < grid.Count(); y2++)
                    {
                        for (int x2 = 0; x2 < grid[y2].Count(); x2++)
                        {
                            if (grid[y2][x2] == curFreq && y2 != y && x2 != x)
                            {
                                int yDiff = y2 - y;
                                int xDiff = x2 - x;
                                int yAnti = y2 + yDiff;
                                int xAnti = x2 + xDiff;

                                if (yAnti >= 0 && yAnti < grid.Count() && xAnti >= 0 && xAnti < grid[y].Count())
                                { antinodes.Add(yAnti + "," + xAnti); }

                                // Part 2

                                int gcd = GCD(Math.Abs(yDiff), Math.Abs(xDiff));
                                int yNodesEvery = yDiff / gcd;
                                int xNodesEvery = xDiff / gcd;

                                for (int p2y = y, p2x = x; p2y < grid.Count() && p2x < grid[y].Count() && p2y >= 0 && p2x >= 0; p2y += yNodesEvery, p2x += xNodesEvery)
                                { p2antinodes.Add(p2y + "," + p2x); }

                                for (int p2y = y, p2x = x; p2y < grid.Count() && p2x < grid[y].Count() && p2y >= 0 && p2x >= 0; p2y -= yNodesEvery, p2x -= xNodesEvery)
                                { p2antinodes.Add(p2y + "," + p2x); }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Part 1: " + antinodes.Distinct().Count());
        Console.WriteLine("Part 2: " + p2antinodes.Distinct().Count());
    }

    static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
            { a %= b; }
            else
            { b %= a; }
        }

        return a | b;
    }
}
