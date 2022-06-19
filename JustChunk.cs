namespace PngAnalyzer
{
    using System;
    using System.IO;
    using System.Linq;

    public class JustChunk : IChunk
    {

        public uint Length { get; private set; }
        public string Type { get; private set; }
        public byte[] Data { get; private set; }
        public uint Crc { get; private set; }

        public static JustChunk Create(Stream stream)
        {
            var length = new byte[4];
            stream.Read(length, 0, length.Length);

            var name = new byte[4];
            stream.Read(name, 0, name.Length);

            var data = new byte[BitConverter.ToUInt32(length.Reverse().ToArray(), 0)];
            stream.Read(data, 0, data.Length);

            var crc = new byte[4];
            stream.Read(crc, 0, crc.Length);

            if (BitConverter.ToUInt32(crc.Reverse().ToArray(), 0) != Crc32.Calculate(name.Concat(data).ToArray()))
            {
                throw new FormatException("CRCチェックエラー");
            }

            return new JustChunk
            {
                Length = BitConverter.ToUInt32(length.Reverse().ToArray(), 0),
                Type = System.Text.Encoding.ASCII.GetString(name),
                Data = data,
                Crc = BitConverter.ToUInt32(crc.Reverse().ToArray(), 0),
            };
        }
    }
}