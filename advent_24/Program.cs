using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace advent_24;

class Program
{
    static void Main(string[] args)
    {
        var inp = File.ReadAllText("input.txt").Split("\n\n");
        List<Gate> gates = [];
        Dictionary<string, int> wires = [];

        foreach (string guh in inp[1].Split("\n"))
        {
            var huh = guh.Split(" ");

            gates.Add(new Gate()
            {
                gate = huh[1],
                wireA = huh[0],
                wireB = huh[2],
                wire_out = huh[4]
            });

            wires[huh[0]] = -1;
            wires[huh[2]] = -1;
            wires[huh[4]] = -1;
        }

        foreach (string nah in inp[0].Split("\n"))
        {
            var bruh = nah.Split(": ");

            wires[bruh[0]] = bruh[1] == "1" ? 1 : 0;
        }

        while (gates.Count > 0)
        {
            gates.Where(gate => wires[gate.wireA] != -1 && wires[gate.wireB] != -1).ToList().ForEach(gate =>
            {
                if (gate.gate == "OR") { wires[gate.wire_out] = (wires[gate.wireA] == 1) || (wires[gate.wireB] == 1) ? 1 : 0; }
                else if (gate.gate == "AND") { wires[gate.wire_out] = (wires[gate.wireA] == 1) && (wires[gate.wireB] == 1) ? 1 : 0; }
                else if (gate.gate == "XOR") { wires[gate.wire_out] = (wires[gate.wireA] == 1) ^ (wires[gate.wireB] == 1) ? 1 : 0; }

                gates.Remove(gate);
            });
        }

        long part1 = Convert.ToInt64(string.Join(string.Empty, wires.Where(w => w.Key.StartsWith('z')).OrderByDescending(w => w.Key[1..]).Select(w => w.Value.ToString())), 2);

        Console.WriteLine("Part 1: " + part1);
    }
}

class Gate
{
    public string gate;
    public string wireA;
    public string wireB;
    public string wire_out;
}
