namespace Problem.HuffmanCoding;

public class HuffmanCodingOutputStep : ProblemOutputStep
{
    public Node? HuffmanTree;
    public  Tuple<char, string>? HuffmanCode;

    public Dictionary<char, string> HuffmanDictionary = new();


    public HuffmanCodingOutputStep(Node? HuffmanTree, Tuple<char, string>? code)
    {
        this.HuffmanTree = HuffmanTree;
        this.HuffmanCode = code;
    }
}