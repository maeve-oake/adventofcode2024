namespace advent_21;

class Program
{
    static Dictionary<string, List<string>> dirRoute = [];
    static Dictionary<string, List<string>> numericRoute = [];
    static Dictionary<string, int> visited = [];
    static List<List<char>> input = [];
    static bool idklol = false; // num = true dir = false;
    static List<List<char>> numinput = File.ReadAllLines("numeric.txt").Select(l => l.ToCharArray().ToList()).ToList();
    static List<List<char>> dirinput = File.ReadAllLines("direction.txt").Select(l => l.ToCharArray().ToList()).ToList();
    static int ly, lx, sy, sx;


    static void Main(string[] args)
    {
        List<string> d21input = File.ReadAllLines("input.txt").ToList();
        int part1 = 0;

        CalcNumericalPaths();
        CalcDirectionalPaths();

        d21input.ForEach(h =>
        {
            string seq = h.Split(" ")[0];
            List<string> seq1 = NumericalKeypad('A' + seq);

            int minLength = seq1.Min(s => s.Length);
            seq1 = seq1.Where(x => x.Length == minLength).ToList();

            List<string> seq2 = [];

            seq1.ForEach(next =>
            {
                seq2.AddRange(DirectionalKeypad('A' + next));
            });

            int minLength2 = seq2.Min(s => s.Length);
            seq2 = seq2.Where(x => x.Length == minLength2).ToList();

            List<string> seq3 = [];

            seq2.ForEach(next =>
            {
                seq3.AddRange(DirectionalKeypad('A' + next));
            });

            int minLength3 = seq3.Min(s => s.Length);
            seq3 = seq3.Where(x => x.Length == minLength3).ToList();

            Console.WriteLine($"{seq}: {seq1}");
            Console.WriteLine($"{seq}: {seq2}");
            Console.WriteLine($"{seq}: {seq3}");
            //Console.WriteLine($"exmp: {h.Split(" ")[1]}");
            part1 += seq3[0].Length * int.Parse(seq.Replace("A", ""));
            Console.WriteLine();
        });

        Console.WriteLine("Part 1: " + part1);
    }

    static char lookupDir(int y, int x)
    {
        char f = '\0';
        if (y == 0)
        {
            if (x == 1) { f = '^'; }
            else if (x == 2) { f = 'A'; }
        }
        else if (y == 1)
        {
            if (x == 0) { f = '<'; }
            else if (x == 1) { f = 'v'; }
            else if (x == 2) { f = '>'; }
        }

        return f;
    }

    static char lookupNum(int y, int x)
    {
        char f = '\0';
        if (y == 0)
        {
            if (x == 0) { f = '7'; }
            else if (x == 1) { f = '8'; }
            else { f = '9'; }
        }
        else if (y == 1)
        {
            if (x == 0) { f = '4'; }
            else if (x == 1) { f = '5'; }
            else { f = '6'; }
        }
        else if (y == 2)
        {
            if (x == 0) { f = '1'; }
            else if (x == 1) { f = '2'; }
            else { f = '3'; }
        }
        else
        {
            if (x == 1) { f = '0'; }
            else if (x == 2) { f = 'A'; }
        }

        return f;
    }

    static Dictionary<string, List<string>> recurse(int y, int x, char dir, int score, string route)
    {
        route += dir;
        visited[$"{y},{x}"] = score;
        int value;
        string key;

        if (y == ly && x == lx)
        {
            //Console.WriteLine("FOUND! " + score);
            if (idklol)
            {
                key = $"{lookupNum(sy - 1, sx - 1)}{lookupNum(ly - 1, lx - 1)}";
                List<string> outv;
                if (!numericRoute.TryGetValue(key, out outv) || outv[0].Length > route.Length - 1) { numericRoute[key] = []; }

                numericRoute[key].Add(route[1..]);
            }
            else
            {
                key = $"{lookupDir(sy - 1, sx - 1)}{lookupDir(ly - 1, lx - 1)}";
                List<string> outv;
                if (!dirRoute.TryGetValue(key, out outv) || outv[0].Length > route.Length - 1) { dirRoute[key] = []; }

                dirRoute[key].Add(route[1..]);
            }

            return idklol ? numericRoute : dirRoute;
        }

        //Console.WriteLine("y: " + y + " x: " + x + " dir: " + dir + " score: " + score);

        if (input[y - 1][x] == '.' && dir != 'v') // up 
        {
            int newscore = score + 1;

            key = $"{y - 1},{x}";
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y - 1, x, '^', newscore, route);
            }
        }

        if (input[y + 1][x] == '.' && dir != '^') // down
        {
            int newscore = score + 1;

            key = $"{y + 1},{x}";
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y + 1, x, 'v', newscore, route);
            }
        }

        if (input[y][x + 1] == '.' && dir != '<') // right
        {
            int newscore = score + 1;

            key = $"{y},{x + 1}";
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y, x + 1, '>', newscore, route);
            }
        }

        if (input[y][x - 1] == '.' && dir != '>') // left 
        {
            int newscore = score + 1;

            key = $"{y},{x - 1}";
            if (!visited.TryGetValue(key, out value) || value >= newscore)
            {
                recurse(y, x - 1, '<', newscore, route);
            }
        }

        return idklol ? numericRoute : dirRoute;
    }

    static void CalcNumericalPaths()
    {
        input = numinput;
        idklol = true;

        /*
        +---+---+---+
        | 7 | 8 | 9 |
        +---+---+---+
        | 4 | 5 | 6 |
        +---+---+---+
        | 1 | 2 | 3 |
        +---+---+---+
            | 0 | A |
            +---+---+
        */
        List<Tuple<int, int>> pos = [];

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                pos.Add(new(y, x));
            }
        }

        pos.Remove(new(3, 0));

        pos.ForEach(posi =>
        {
            int y = posi.Item1;
            int x = posi.Item2;

            pos.ForEach(end =>
            {
                ly = end.Item1 + 1;
                lx = end.Item2 + 1;
                sy = y + 1;
                sx = x + 1;

                visited = [];

                recurse(y + 1, x + 1, 'h', 0, "");
            });
        });
    }

    static void CalcDirectionalPaths()
    {
        input = dirinput;
        idklol = false;

        /*
            +---+---+
            | ^ | A |
        +---+---+---+
        | < | v | > |
        +---+---+---+
        */

        List<Tuple<int, int>> pos = [new(0, 1), new(0, 2), new(1, 0), new(1, 1), new(1, 2)]; // fuck this

        pos.ForEach(posi =>
        {
            int y = posi.Item1;
            int x = posi.Item2;

            char f = lookupDir(y, x);

            pos.ForEach(end =>
            {
                ly = end.Item1 + 1;
                lx = end.Item2 + 1;
                sy = y + 1;
                sx = x + 1;

                visited = [];

                recurse(y + 1, x + 1, 'h', 0, "");
            });

        });
    }

    static List<string> DirectionalKeypad(string seq, string path = "")
    {
        List<string> all = [];
        if (seq == "A")
        {
            all.Add(path);
            return all;
        }

        dirRoute[seq[..2]].ForEach(miaow =>
        {
            all.AddRange(DirectionalKeypad(seq[1..], path + miaow + 'A'));
        });

        return all;
    }

    static List<string> NumericalKeypad(string seq, string path = "")
    {
        List<string> all = [];
        if (seq == "A")
        {
            all.Add(path);
            return all;
        }

        numericRoute[seq[..2]].ForEach(miaow =>
        {
            all.AddRange(NumericalKeypad(seq[1..], path + miaow + 'A'));
        });

        return all;
    }
}
