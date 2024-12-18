namespace advent_18;

class Program
{
    static Dictionary<int, int> visited = [];
    static List<List<char>> grid = [];
    static Stack<Tuple<int, int, char, int>> queue = [];

    static int SIZE = 71;
    static int bigscore = Int32.MaxValue;

    static void Main(string[] args)
    {
        List<Tuple<int, int>> input = File.ReadAllLines("input.txt").Select(x => x.Split(",").Select(int.Parse)).Select(x => { return new Tuple<int, int>(x.First(), x.Last()); }).ToList();
        grid = Enumerable.Range(0, SIZE + 2).Select(i => Enumerable.Repeat('.', SIZE).Append('#').Prepend('#').ToList()).ToList();

        grid[0] = Enumerable.Repeat('#', SIZE + 2).ToList();
        grid[^1] = Enumerable.Repeat('#', SIZE + 2).ToList();

        for (int i = 0; i < 1024; i++)
        {
            grid[input[i].Item2 + 1][input[i].Item1 + 1] = '#';
        }

        foreach (List<char> line in grid)
        {
            foreach (char character in line)
            {
                Console.Write(character);
            }
            Console.WriteLine();
        }

        queue.Push(new(1, 1, '>', 0));

        while (queue.Count > 0)
        {
            Tuple<int, int, char, int> h = queue.Pop();
            recurse(h.Item1, h.Item2, h.Item3, h.Item4);
        }

        Console.WriteLine("Part 1: " + bigscore);

        for (int i = 1024; i < input.Count; i++)
        {
            grid[input[i].Item2 + 1][input[i].Item1 + 1] = '#';

            bigscore = Int32.MaxValue;
            visited = [];
            queue.Push(new(1, 1, '>', 0));
            while (queue.Count > 0)
            {
                Tuple<int, int, char, int> h = queue.Pop();
                recurse(h.Item1, h.Item2, h.Item3, h.Item4);
            }

            Console.WriteLine($"attempt: {input[i].Item1},{input[i].Item2}");

            if (bigscore == Int32.MaxValue)
            {
                Console.WriteLine($"Part 2: {input[i].Item1},{input[i].Item2}");
                break;
            }
        }
    }

    static int recurse(int y, int x, char dir, int score)
    {
        if (score >= bigscore) { return 0; }

        visited[(y * SIZE) + x] = score;

        if (y == SIZE && x == SIZE)
        {
            //Console.WriteLine("FOUND! " + score);
            if (score < bigscore) { bigscore = score; }
            return score;
        }
        int value;
        int key;

        if (grid[y + 1][x] == '.' && dir != '^') // down
        {
            int newscore = score + 1;

            key = ((y + 1) * SIZE) + x;
            if (!visited.TryGetValue(key, out value) || value > newscore)
            {
                queue.Push(new(y + 1, x, 'v', newscore));
            }
        }

        if (grid[y][x + 1] == '.' && dir != '<') // right
        {
            int newscore = score + 1;

            key = (y * SIZE) + x + 1;
            if (!visited.TryGetValue(key, out value) || value > newscore)
            {
                queue.Push(new(y, x + 1, '>', newscore));
            }
        }

        if (grid[y - 1][x] == '.' && dir != 'v') // up 
        {
            int newscore = score + 1;

            key = ((y - 1) * SIZE) + x;
            if (!visited.TryGetValue(key, out value) || value > newscore)
            {
                queue.Push(new(y - 1, x, '^', newscore));
            }
        }

        if (grid[y][x - 1] == '.' && dir != '>') // left 
        {
            int newscore = score + 1;

            key = (y * SIZE) + x - 1;
            if (!visited.TryGetValue(key, out value) || value > newscore)
            {
                queue.Push(new(y, x - 1, '<', newscore));
            }
        }

        return 0;
    }
}
