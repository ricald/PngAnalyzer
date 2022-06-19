namespace PngAnalyzer
{
    using System;
    using System.IO;

    public class Signature
    {
        public static Signature Create(Stream stream)
        {
            var expected = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            var signature = new byte[8];
            stream.Read(signature, 0, signature.Length);

            for (int i = 0; i < signature.Length; i++)
            {
                if (expected[i] != signature[i])
                {
                    throw new FormatException("PNG識別子ではありません");
                }
            }

            return new Signature
            {
            };
        }
    }
}