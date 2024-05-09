using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingOutput : ProblemOutput
{
    public string result = "";
    public Dictionary<char, string> huffmanDictionary = new();
}

