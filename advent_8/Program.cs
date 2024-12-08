namespace advent_8;

class Program
{
    static void Main(string[] args)
    {
        List<List<char>> grid = File.ReadAllLines("p2test.txt").Select(x => x.ToList()).ToList();
        List<string> antinodes = [];

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
                                {
                                    antinodes.Add(yAnti + "," + xAnti);
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Part 1: " + antinodes.Distinct().Count());
    }
}
