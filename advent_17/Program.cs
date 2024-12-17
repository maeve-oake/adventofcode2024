namespace advent_17;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllText("input.txt").Split("\n\n");
        string[] registers = input[0].Split("\n");

        long A = long.Parse(registers[0].Replace("Register A: ", ""));
        long B = long.Parse(registers[1].Replace("Register B: ", ""));
        long C = long.Parse(registers[2].Replace("Register C: ", ""));

        string testforp2 = input[1].Split("m: ")[1] + ",";
        List<int> program = input[1].Split("m: ")[1].Split(",").Select(int.Parse).ToList();

        /*  COMBO OPERAND:

            0 through 3 are LITERAL.
            4 = Register A
            5 = Register B
            6 = Register C
            7 is RESERVED
        */

        Console.WriteLine("Part 1: " + Run(program, A, B, C));

        /*  INSTRUCTIONS:

            0 - Division:
            Numerator: A
            Denominator: 2 ^ COMBO
            RESULT: truncate longo register A

            1 - Bitwise XOR
            register B XOR LITERAL
            RESULT: longo register B

            2 - Modulo 8:
            COMBO Modulo 8
            RESULT: longo register B

            3 - Jump not zero:
            if register A is ZERO do NOTHING
            else set polonger to LITERAL
            DO NOT INCREASE POlongER!

            4 - Bitwise XOR:
            register B XOR register C
            RESULT: longo register B

            5 - Modulo 8:
            COMBO modulo 8
            RESULT: Output value (multiple seperated by commas)

            6 - Division:
            Numerator: A
            Denominator: 2 ^ COMBO
            RESULT: truncate longo register B

            7 - Division:
            Numerator: A
            Denominator: 2 ^ COMBO
            RESULT: truncate longo register C
        */
    }

    static string Run(List<int> program, long A, long B, long C)
    {
        string output = "";

        for (int i = 0; i < program.Count;)
        {
            //Console.WriteLine($"A: {A}, B: {B}, C: {C}, op {program[i]} val {program[i + 1]}");
            switch (program[i])
            {
                case 0: // division, (A / 2^combo) => A
                    A = A / (long)Math.Pow(2, Combo(program[i + 1], A, B, C));
                    i += 2;
                    break;

                case 1: // bitwise XOR, (B XOR LITERAL) => B
                    B = B ^ program[i + 1];
                    i += 2;
                    break;

                case 2: // Mod 8, (combo % 8) => B
                    B = Combo(program[i + 1], A, B, C) % 8;
                    i += 2;
                    break;

                case 3:
                    if (A != 0) { i = program[i + 1]; } else { i += 2; }
                    break;

                case 4:
                    B = B ^ C;
                    i += 2;
                    break;

                case 5:
                    output += (Combo(program[i + 1], A, B, C) % 8) + ",";
                    i += 2;
                    break;

                case 6:
                    B = A / (long)Math.Pow(2, Combo(program[i + 1], A, B, C));
                    i += 2;
                    break;

                case 7:
                    C = A / (long)Math.Pow(2, Combo(program[i + 1], A, B, C));
                    i += 2;
                    break;
            }
        }

        return output;
    }

    static long Combo(long operand, long A, long B, long C)
    {
        switch (operand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return operand;
            case 4:
                return A;
            case 5:
                return B;
            case 6:
                return C;
        }

        return operand;
    }
}
