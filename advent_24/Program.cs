using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace advent_24;

class Program
{
    static void Main(string[] args)
    {
        var inp = File.ReadAllText("input.txt").Split("\n\n");
        List<Gate> gates = [];
        Dictionary<string, bool> wires = [];

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
        }

        foreach (string nah in inp[0].Split("\n"))
        {
            var bruh = nah.Split(": ");
            wires[bruh[0]] = bruh[1] == "1" ? true : false;
        }

        while (gates.Count > 0)
        {
            gates.Where(gate => wires.TryGetValue(gate.wireA, out _) && wires.TryGetValue(gate.wireB, out _)).ToList().ForEach(gate =>
            {
                if (gate.gate == "OR") { wires[gate.wire_out] = wires[gate.wireA] || wires[gate.wireB]; }
                else if (gate.gate == "AND") { wires[gate.wire_out] = wires[gate.wireA] && wires[gate.wireB]; }
                else if (gate.gate == "XOR") { wires[gate.wire_out] = wires[gate.wireA] ^ wires[gate.wireB]; }

                gates.Remove(gate);
            });
        }

        long part1 = Convert.ToInt64(string.Join(string.Empty, wires.Where(w => w.Key.StartsWith('z')).OrderByDescending(w => w.Key[1..]).Select(w => w.Value ? "1" : "0")), 2);

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
