namespace PngAnalyzer
{
    public interface IChunk
    {
        uint Length { get; }
        string Type { get; }
        uint Crc { get; }

        string ToString();
    }
}