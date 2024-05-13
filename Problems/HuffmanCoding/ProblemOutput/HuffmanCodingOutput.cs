using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingOutput : ProblemOutput
{
    public string Result = "";

    public string InputPhrase = "";
    public Dictionary<char, string> HuffmanDictionary = new();
}

