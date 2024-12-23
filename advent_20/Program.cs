namespace advent_20;

class Program
{
    static void Main(string[] args)
    {
        List<List<char>> input = File.ReadAllLines("test.txt").Select(x => x.ToCharArray().ToList()).ToList();
    }
}
