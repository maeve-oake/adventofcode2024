using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
namespace advent_14;

class Program
{
    static void Main(string[] args)
    {
        int tileX = 101;
        int tileY = 103;

        List<Robot> robots = File.ReadAllLines("input.txt").Select(x =>
        {
            return new Robot
            {
                px = int.Parse(x.Split(" ")[0].Replace("p=", "").Split(",")[0]),
                py = int.Parse(x.Split(" ")[0].Replace("p=", "").Split(",")[1]),
                vx = int.Parse(x.Split(" ")[1].Replace("v=", "").Split(",")[0]),
                vy = int.Parse(x.Split(" ")[1].Replace("v=", "").Split(",")[1])
            };
        }).ToList();

        for (int i = 0; i < 30000; i++)
        {
            PngEncoder Encoder = new()
            {
                BitDepth = PngBitDepth.Bit1,
                ColorType = PngColorType.Palette,
                CompressionLevel = PngCompressionLevel.BestCompression,
                FilterMethod = PngFilterMethod.Adaptive,
            };

            Image<Argb32> image = new(tileX, tileY);
            image.ProcessPixelRows(acc =>
            {
                for (int y = 0; y < tileY; y++)
                {
                    Image<Argb32> image = new(tileX, tileY);

                    string line = "";
                    for (int x = 0; x < tileX; x++)
                    {
                        if (robots.Where(r => r.px == x && r.py == y).Any())
                        { acc.GetRowSpan(y)[x] = new Argb32(0xFF, 0xFF, 0xFF); }
                        else { acc.GetRowSpan(y)[x] = new Argb32(0x00, 0x00, 0x00); }
                    }
                }
            });

            image.Save("frames/" + i + ".png", Encoder);

            // basically run this until you have a billion images, then sort by size
            // the smallest image should be the one with the least entropy, which compresses the best, and therefore has the tree!
            // thanks to anya for being this smart :3

            robots.ForEach(robot =>
            {
                robot.px = (robot.px + robot.vx) % tileX;
                robot.py = (robot.py + robot.vy) % tileY;

                if (robot.px < 0) { robot.px = tileX + robot.px; }
                if (robot.py < 0) { robot.py = tileY + robot.py; }
            });

        }

        int q1 = 0, q2 = 0, q3 = 0, q4 = 0;

        robots.ForEach(robot =>
        {
            if (robot.py < tileY / 2)
            {
                if (robot.px < tileX / 2) { q1++; }
                else if (robot.px > tileX / 2) { q2++; }
            }
            else if (robot.py > tileY / 2)
            {
                if (robot.px < tileX / 2) { q3++; }
                else if (robot.px > tileX / 2) { q4++; }
            }
        });

        int part1 = q1 * q2 * q3 * q4;

        Console.WriteLine("Part 1: " + part1);

    }

}

class Robot
{
    public int px;
    public int py;

    public int vx;
    public int vy;
}
