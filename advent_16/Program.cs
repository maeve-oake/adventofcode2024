namespace advent_16;

class Program
{
    static List<List<char>> input = File.ReadAllLines("test.txt").Select(l => l.ToCharArray().ToList()).ToList();
    static Dictionary<Tuple<int, int>, int> visited = [];

    static int part1 = int.MaxValue;

    static void Main(string[] args)
    {
        int start_y = input.IndexOf(input.Where(l => l.Contains('S')).First());
        int start_x = input.Where(l => l.Contains('S')).First().IndexOf('S');

        input[start_y][start_x] = 'O';

        // int end_y = input.IndexOf(input.Where(l => l.Contains('E')).First());
        // int end_x = input.Where(l => l.Contains('E')).First().IndexOf('E');

        // FUCKING DIJKSTRA?????? BREADTH FIRST DIRECTED WEIGHTED NODE GRAPH  AAAAA (a leve puter skienc....)

        // i guess we gotta find the way to each "turn" from the current pos, then find which is the shortest then shortest then shortest...
        // i dont think this algo is supposed to handle dead ends tho... hm,
        // i guess we just say that node's length is inf lol (but that's what we start with!! im supposed to go fix some dumbass broken docking station in half an hour graaahh)
        // turns out her laptop had severe water damage LOL

        // i need some data structure to store these nodes. weight, direction, other nodes.....

        visited.Add(new Tuple<int, int>(start_y, start_x), 0);
        recurse(start_y, start_x, '>', 0);

        Console.WriteLine("Part 1: " + part1);
    }

    static int recurse(int y, int x, char dir, int score)
    {
        int value;
        Tuple<int, int> key;

        //Console.WriteLine("y: " + y + " x: " + x + " dir: " + dir + " score: " + score);

        if (input[y - 1][x] == '.' && dir != 'v') // up 
        {
            int newscore = dir == '^' ? score + 1 : score + 1001;

            key = new(y - 1, x);
            if (visited.TryGetValue(key, out value))
            {
                if (newscore < value)
                {
                    visited[key] = newscore;
                    recurse(y - 1, x, '^', newscore);
                }
            }
            else
            {
                visited.Add(key, newscore);
                recurse(y - 1, x, '^', newscore);
            }
        }

        if (input[y + 1][x] == '.' && dir != '^') // down
        {
            int newscore = dir == 'v' ? score + 1 : score + 1001;

            key = new(y + 1, x);
            if (visited.TryGetValue(key, out value))
            {
                if (newscore < value)
                {
                    visited[key] = newscore;
                    recurse(y + 1, x, 'v', newscore);
                }
            }
            else
            {
                visited.Add(key, newscore);
                recurse(y + 1, x, 'v', newscore);
            }
        }

        if (input[y][x + 1] == '.' && dir != '<') // right
        {
            int newscore = dir == '>' ? score + 1 : score + 1001;

            key = new(y, x + 1);
            if (visited.TryGetValue(key, out value))
            {
                if (newscore < value)
                {
                    visited[key] = newscore;
                    recurse(y, x + 1, '>', newscore);
                }
            }
            else
            {
                visited.Add(key, newscore);
                recurse(y, x + 1, '>', newscore);
            }
        }

        if (input[y][x - 1] == '.' && dir != '>') // left 
        {
            int newscore = dir == '<' ? score + 1 : score + 1001;

            key = new(y, x - 1);
            if (visited.TryGetValue(key, out value))
            {
                if (newscore < value)
                {
                    visited[key] = newscore;
                    recurse(y, x - 1, '<', newscore);
                }
            }
            else
            {
                visited.Add(key, newscore);
                recurse(y, x - 1, '<', newscore);
            }
        }

        if (input[y - 1][x] == 'E' || input[y + 1][x] == 'E' || input[y][x - 1] == 'E' || input[y][x + 1] == 'E')
        {
            Console.WriteLine("FOUND! " + (score + 1));
            if (score + 1 < part1) { part1 = score + 1; }
            return score + 1;
        }

        return 0;
    }
}