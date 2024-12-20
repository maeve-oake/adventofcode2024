﻿using System.Text.RegularExpressions;

namespace advent_3;

class Program
{
    static void Main(string[] args)
    {
        // Part 1
        int p1result = Regex.Matches(File.ReadAllText("input.txt"), @"(?<=mul\()[0-9]+,[0-9]+(?=\))").Cast<Match>().Select(p => p.Value.Split(",").Select(int.Parse).Aggregate(1, (acc, x) => acc * x)).Sum();
        Console.WriteLine("Part 1: " + p1result);

        // Part 2
        int p2result = 0;
        bool doit = true;
        Regex.Matches(File.ReadAllText("input.txt"), @"(?<=mul\()[0-9]+,[0-9]+(?=\))|(don't\(\))|(do\(\))").Cast<Match>().Select(p => p.Value).ToList().ForEach(instr =>
        {
            if (instr == "do()") { doit = true; }
            else if (instr == "don't()") { doit = false; }
            else if (doit) { p2result += int.Parse(instr.Split(",")[0]) * int.Parse(instr.Split(",")[1]); }
        });

        Console.WriteLine("Part 2: " + p2result);
    }
}