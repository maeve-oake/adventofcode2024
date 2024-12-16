namespace advent_16_p2;

class Program
{
    static List<List<char>> input = File.ReadAllLines("input.txt").Select(l => l.ToCharArray().ToList()).ToList();
    static Dictionary<int, int> visited = []; // this is all the visited squares

    static List<List<int>> part2 = []; // this is the list of final paths
    static int bestScore = int.MaxValue;
    static int width = input[0].Count;

    static void Main(string[] args)
    {
        int start_y = input.IndexOf(input.Where(l => l.Contains('S')).First());
        int start_x = input.Where(l => l.Contains('S')).First().IndexOf('S');

        recurse(start_y, start_x, '>', 0, []);

        // for (int i = 0; i < part2[^1].Count; i++)
        // {
        //     Console.WriteLine($"Y:{part2[^1][i].Item1},{part2[^2][i].Item1}"); //-- love this feature i love you
        //     Console.WriteLine("");
        // }

        var miaow = part2.SelectMany(l => l).Distinct().Count() + 1;
        Console.WriteLine("Part 2: " + miaow);

        //Console.WriteLine(string.Join('\n', visited.Select(x => $"{x.Key.Item1},{x.Key.Item2},{x.Key.Item3} - {x.Value}")));
    }

    static int getCacheKey(int y, int x, char dir, int width)
    {
        int d = 0;
        switch (dir)
        {
            case '>':
                d = 1;
                break;
            case 'v':
                d = 2;
                break;
            case '<':
                d = 3;
                break;
        }
        return y * width * 4 + x * 4 + d;
    }

    static void recurse(int y, int x, char dir, int score, List<int> route)
    {
        route.Add(getCacheKey(y, x, '^', width));
        visited[getCacheKey(y, x, dir, width)] = score;

        if (input[y - 1][x] == 'E' || input[y + 1][x] == 'E' || input[y][x - 1] == 'E' || input[y][x + 1] == 'E')
        {
            if (score + 1 > bestScore)
            {
                return;
            }
            if (score + 1 < bestScore)
            {
                Console.WriteLine("FOUND! " + (score + 1));
                bestScore = score + 1;
                part2 = [];
            }
            part2.Add(route);
            return;
        }

        int value;
        int key;

        //Console.WriteLine("y: " + y + " x: " + x + " dir: " + dir + " score: " + score);

        if (input[y - 1][x] == '.' && dir != 'v') // up 
        {
            int newscore = dir == '^' ? score + 1 : score + 1001;

            key = getCacheKey(y - 1, x, '^', width);
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y - 1, x, '^', newscore, new(route));
            }
        }

        if (input[y + 1][x] == '.' && dir != '^') // down
        {
            int newscore = dir == 'v' ? score + 1 : score + 1001;

            key = getCacheKey(y + 1, x, 'v', width);
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y + 1, x, 'v', newscore, new(route));
            }
        }

        if (input[y][x + 1] == '.' && dir != '<') // right
        {
            int newscore = dir == '>' ? score + 1 : score + 1001;

            key = getCacheKey(y, x + 1, '>', width);
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y, x + 1, '>', newscore, new(route));
            }
        }

        if (input[y][x - 1] == '.' && dir != '>') // left 
        {
            int newscore = dir == '<' ? score + 1 : score + 1001;

            key = getCacheKey(y, x - 1, '<', width);
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y, x - 1, '<', newscore, new(route));
            }
        }

        return;
    }
}