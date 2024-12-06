using System.Text;

namespace advent_6;

class Program
{
    static void Main(string[] args)
    {
        List<string> map = File.ReadAllLines("input.txt").ToList();
        List<string> visited = route(map);

        Console.WriteLine("Part 1: " + visited.Distinct().Count());

        visited = visited.Distinct().ToList();
        int y = map.IndexOf(map.Find(x => x.Contains('^')));
        int x = map[y].IndexOf('^');

        visited.RemoveAll(j => int.Parse(j.Split(",")[0]) < y && int.Parse(j.Split(",")[1]) == x); // remove places in LOS of the guard's start
        visited.Remove(y + "," + x);

        int p2 = 0;

        for (int i = 0; i < visited.Count(); i++)
        {
            List<string> p2map = new List<string>(map);

            StringBuilder temp = new StringBuilder(map[int.Parse(visited[i].Split(",")[0])]);
            temp[int.Parse(visited[i].Split(",")[1])] = '#';
            p2map[int.Parse(visited[i].Split(",")[0])] = temp.ToString();

            Console.WriteLine(i + "/" + visited.Count());
            if (route(p2map).Count() == 0) { p2++; };
        }

        Console.WriteLine("Part 2: " + p2);
    }

    static List<string> route(List<string> map)
    {
        List<string> visited = [];
        int turns = 0;
        int y = map.IndexOf(map.Find(x => x.Contains('^')));
        int x = map[y].IndexOf('^');
        visited.Add(y + "," + x);

        bool loop = true;

        while (loop)
        {
            switch (turns % 4 + 1)
            {
                case 1: // up
                    if (y == 0) { loop = false; break; }
                    if (map[y - 1][x] == '#') { turns++; x++; }
                    else { y--; }
                    break;
                case 2: // right
                    if (x + 1 == map[0].Count()) { loop = false; break; }
                    if (map[y][x + 1] == '#') { turns++; y++; }
                    else { x++; }
                    break;
                case 3: // down
                    if (y + 1 == map.Count()) { loop = false; break; }
                    if (map[y + 1][x] == '#') { turns++; x--; }
                    else { y++; }
                    break;
                case 4: // left
                    if (x == 0) { loop = false; break; }
                    if (map[y][x - 1] == '#') { turns++; y--; }
                    else { x--; }
                    break;
            }

            visited.Add(y + "," + x);

            if (visited.Distinct().Count() < visited.Count() / 2) { return new List<string>(); } // just return nothing if it loops
        }

        return visited;
    }
}
