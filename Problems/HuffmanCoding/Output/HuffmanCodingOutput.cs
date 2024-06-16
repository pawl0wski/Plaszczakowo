using Plaszczakowo.ProblemResolver;

namespace Plaszczakowo.Problems.HuffmanCoding.Output;

public record HuffmanCodingOutput : ProblemOutput
{
    public string Result { get; set; } = "";

    public string InputPhrase { get; set; } = "";
    public Dictionary<char, string> HuffmanDictionary { get; set; } = new();
}