namespace advent_9;

class Program
{
    static void Main(string[] args)
    {
        int[] input = File.ReadAllText("test.txt").ToCharArray().Select(x => x - 48).ToArray();
        long[] filesystem = [];
        long output = 0;

        for (long i = 0; i < input.Length; i++) // idx 0,2,4 is files // idx 1,3,5 are free space
        {
            for (long j = 0; j < input[i]; j++)
            {
                filesystem = filesystem.Append(i % 2 == 0 ? i / 2 : -1).ToArray();
            }
        }

        for (long i = 0, j = filesystem.Length; i < filesystem.Where(x => x != -1).Count(); i++)
        {
            if (filesystem[i] == -1)
            {
                if (filesystem.Last() == -1) { filesystem[^1]. }
                filesystem[i] = filesystem.Last();
                filesystem.RemoveAt(filesystem.Count - 1);
            }
        }

        // for (long i = 0; i < filesystem.Count; i++)
        // {
        //     if (filesystem[i] != -1) { output += i * filesystem[i]; };
        // }

        Console.WriteLine("Part 1: " + output);

        // 7879697024069
        // 7879185628361
    }
}
