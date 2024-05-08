using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingOutput : ProblemOutput
{
    public Node? HuffmanTree;
    public  List<Tuple<char, string>> HuffmanCode = new();

    public Dictionary<char, string> HuffmanDictionary = new();

    public string result = "";
}

