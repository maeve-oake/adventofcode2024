namespace advent_13;

class Program
{
    static void Main(string[] args)
    {
        int part1 = 0;
        List<Game> games = File.ReadAllText("test.txt").Split("\n\n").Select(l =>
        {
            return new Game()
            {
                a_x = int.Parse(l.Split("\n")[0].Split(": ")[1].Split(", ")[0].Replace("X+", "")),
                a_y = int.Parse(l.Split("\n")[0].Split(": ")[1].Split(", ")[1].Replace("Y+", "")),

                b_x = int.Parse(l.Split("\n")[1].Split(": ")[1].Split(", ")[0].Replace("X+", "")),
                b_y = int.Parse(l.Split("\n")[1].Split(": ")[1].Split(", ")[1].Replace("Y+", "")),

                prize_x = int.Parse(l.Split("\n")[2].Split(": ")[1].Split(", ")[0].Replace("X=", "")),
                prize_y = int.Parse(l.Split("\n")[2].Split(": ")[1].Split(", ")[1].Replace("Y=", "")),
            };
        }).ToList();

        games.ForEach(game =>
        {

        });

        Console.WriteLine("Part 1: ");
    }

    static int gcf(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static int lcm(int a, int b)
    {
        return (a / gcf(a, b)) * b;
    }
}

class Game
{
    public int a_x;
    public int a_y;

    public int b_x;
    public int b_y;

    public int prize_x;
    public int prize_y;
}


