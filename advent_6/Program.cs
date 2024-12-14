namespace advent_6;

class Program
{
    static void Main(string[] args)
    {
        List<string> map = File.ReadAllLines("input.txt").ToList();
        List<Tuple<int, int>> visited = route(map);

        Console.WriteLine("Part 1: " + visited.Distinct().Count());

        visited = visited.Distinct().ToList();
        int y = map.IndexOf(map.Find(x => x.Contains('^')));
        int x = map[y].IndexOf('^');

        visited.Remove(new Tuple<int, int>(y, x)); // remove guard's starting position

        int p2 = 0;

        for (int i = 0; i < visited.Count; i++)
        {
            if (route(map, visited[i]) is null) { p2++; };
        }

        Console.WriteLine("Part 2: " + p2);
    }

    static List<Tuple<int, int>> route(List<string> map, Tuple<int, int>? extra = null)
    {
        Dictionary<Tuple<int, int>, int> visited = [];

        int turns = 0;
        int y = map.IndexOf(map.Find(x => x.Contains('^')));
        int x = map[y].IndexOf('^');
        visited[new Tuple<int, int>(y, x)] = 1;

        bool loop = true;

        while (loop)
        {
            switch (turns % 4 + 1)
            {
                case 1: // up
                    if (y == 0) { loop = false; break; }
                    if (map[y - 1][x] == '#' || (extra is not null && (y - 1) == extra.Item1 && x == extra.Item2)) { turns++; x++; }
                    else { y--; }
                    break;
                case 2: // right
                    if (x + 1 == map[0].Length) { loop = false; break; }
                    if (map[y][x + 1] == '#' || (extra is not null && (y == extra.Item1) && (x + 1) == extra.Item2)) { turns++; y++; }
                    else { x++; }
                    break;
                case 3: // down
                    if (y + 1 == map.Count) { loop = false; break; }
                    if (map[y + 1][x] == '#' || (extra is not null && (y + 1) == extra.Item1 && x == extra.Item2)) { turns++; x--; }
                    else { y++; }
                    break;
                case 4: // left
                    if (x == 0) { loop = false; break; }
                    if (map[y][x - 1] == '#' || (extra is not null && (y == extra.Item1) && (x - 1) == extra.Item2)) { turns++; y--; }
                    else { x--; }
                    break;
            }

            if (loop)
            {
                Tuple<int, int> cspace = new(y, x);

                int value;
                visited.TryGetValue(cspace, out value);
                if (value > 10) { return null; }

                visited[cspace] = value + 1;
            }
        }

        return visited.Keys.ToList();
    }
}
