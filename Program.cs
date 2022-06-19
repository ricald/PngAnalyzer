namespace PngAnalyzer
{
    using System;
    using System.IO;

    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("ファイル名を指定してください");
            }

            using (var fs = new FileStream(args[0], FileMode.Open))
            {
                var signature = Signature.Create(fs);

                while (fs.Position < fs.Length)
                {
                    var justChunk = JustChunk.Create(fs);
                    Console.WriteLine($"Length={justChunk.Length},Type={justChunk.Type},CRC={justChunk.Crc}");

                    IChunk chunk = justChunk.Type switch
                    {
                        "IHDR" => Ihdr.Create(justChunk.Length, justChunk.Type, new MemoryStream(justChunk.Data), justChunk.Crc),
                        "PLTE" => Plte.Create(justChunk.Length, justChunk.Type, new MemoryStream(justChunk.Data), justChunk.Crc),
                        "IDAT" => Idat.Create(justChunk.Length, justChunk.Type, new MemoryStream(justChunk.Data), justChunk.Crc),
                        _ => null,
                    };

                    if (chunk == null)
                    {
                        continue;
                    }

                    Console.WriteLine(chunk.ToString());
                }
            }
        }
    }
}
