namespace PngAnalyzer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Ihdr : IChunk
    {
        public uint Length { get; private set; }
        public string Type { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public byte Depth { get; private set; }
        public byte ColorType { get; private set; }
        public byte CompressionMethod { get; private set; }
        public byte FilterMethod { get; private set; }
        public byte InterlaceMethod { get; private set; }
        public uint Crc { get; private set; }

        public static Ihdr Create(uint length, string type, Stream stream, uint crc)
        {
            var width = new byte[4];
            stream.Read(width, 0, width.Length);

            var height = new byte[4];
            stream.Read(height, 0, height.Length);

            var depth = (byte)stream.ReadByte();

            var colorType = (byte)stream.ReadByte();

            var compressionType = (byte)stream.ReadByte();

            var filterType = (byte)stream.ReadByte();

            var interlaceType = (byte)stream.ReadByte();

            return new Ihdr
            {
                Length = length,
                Type = type,
                Width = BitConverter.ToUInt32(width.Reverse().ToArray(), 0),
                Height = BitConverter.ToUInt32(height.Reverse().ToArray(), 0),
                Depth = depth,
                ColorType = colorType,
                CompressionMethod = compressionType,
                FilterMethod = filterType,
                InterlaceMethod = interlaceType,
                Crc = crc,
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Width={this.Width}");
            sb.AppendLine($"Height={this.Height}");
            sb.AppendLine($"Depth={this.Depth}");
            sb.AppendLine($"ColorType={this.ColorType}");
            sb.AppendLine($"CompressionMethod={this.CompressionMethod}");
            sb.AppendLine($"FilterMethod={this.FilterMethod}");
            sb.AppendLine($"InterlaceMethod={this.InterlaceMethod}");

            return sb.ToString();
        }
    }
}