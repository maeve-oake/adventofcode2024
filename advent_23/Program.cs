using System.ComponentModel;

namespace advent_23;

class Program
{
    static void Main(string[] args)
    {
        List<string> input = File.ReadAllLines("input.txt").ToList();
        List<string> threes = [];
        int part1 = 0;


        input.ForEach(l =>
        {
            var link = l.Split("-");
            foreach (string l2 in input.Where(x => x.Contains(link[1]) && x != l))
            {
                string look = l2.Split("-")[l2.StartsWith(link[1]) ? 1 : 0];
                foreach (string l3 in input.Where(x => x.Contains(look) && x.Contains(link[0]) && x != l && x != l2))
                {
                    if (!threes.Where(x => x.Contains(link[0]) && x.Contains(link[1]) && x.Contains(look)).Any())
                    {
                        threes.Add(l + "-" + look);
                        //Console.WriteLine(threes.Last());
                        if (link[0].StartsWith('t') || link[1].StartsWith('t') || look.StartsWith('t')) { part1++; }
                    }
                }
            }
        });

        Console.WriteLine("Part 1: " + part1);
    }
}
