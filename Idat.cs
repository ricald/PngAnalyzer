namespace PngAnalyzer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Idat : IChunk
    {
        public uint Length { get; private set; }
        public string Type { get; private set; }
        public byte Flag { get; private set; }
        public byte Additional { get; private set; }
        public byte[] Data { get; private set; }
        public uint Check { get; private set; }
        public uint Crc { get; private set; }

        public static Idat Create(uint length, string type, Stream stream, uint crc)
        {
            var flag = (byte)stream.ReadByte();

            var additional = (byte)stream.ReadByte();

            var data = new byte[stream.Length - 1 - 1 - 4];
            stream.Read(data, 0, data.Length);

            var check = new byte[4];
            stream.Read(check, 0, check.Length);

            return new Idat
            {
                Length = length,
                Type = type,
                Flag = flag,
                Additional = additional,
                Data = data,
                Check = BitConverter.ToUInt32(check.Reverse().ToArray(), 0),
                Crc = crc,
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flag={this.Flag}");
            sb.AppendLine($"Additional={this.Additional}");
            sb.AppendLine($"Check={this.Check}");

            return sb.ToString();
        }
    }
}