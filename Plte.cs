namespace PngAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;

    public class Plte : IChunk
    {
        public uint Length { get; private set; }
        public string Type { get; private set; }
        public IReadOnlyList<Color> Pallettes { get; private set; }
        public uint Crc { get; private set; }

        public static Plte Create(uint length, string type, Stream stream, uint crc)
        {
            if (stream.Length % 3 != 0)
            {
                throw new FormatException("PLTEのサイズが3で割り切れない");
            }

            var pallettes = new List<Color>();
            while (stream.Position < stream.Length)
            {
                var r = stream.ReadByte();
                var g = stream.ReadByte();
                var b = stream.ReadByte();
                pallettes.Add(Color.FromArgb(r, g, b));
            }

            return new Plte
            {
                Length = length,
                Type = type,
                Pallettes = pallettes,
                Crc = crc,
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int index = 0; index < this.Pallettes.Count; index++)
            {
                sb.AppendLine($"Entry={index + 1},R={this.Pallettes[index].R},G={this.Pallettes[index].G},B={this.Pallettes[index].B}");
            }

            return sb.ToString();
        }
    }
}