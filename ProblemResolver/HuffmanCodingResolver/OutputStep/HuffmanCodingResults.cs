namespace Problem.HuffmanCoding;

public class HuffmanCodingResults : ProblemResults
{

    public Dictionary<char, int> LetterAppearances;
    public List<Node> MinHeap;
    public Node? HuffmanTree;
    public  Dictionary<char, string> HuffmanDictionary;


    public HuffmanCodingResults()
    {
        this.LetterAppearances = new();
        this.MinHeap = new();
        this.HuffmanDictionary = new();
    }
}