namespace Problem.HuffmanCoding;

public class HuffmanCodingOutputStep : ProblemOutputStep
{

    public Dictionary<char, int> LetterAppearances;
    public PriorityQueue<Node, int> MinHeap;
    public Node? HuffmanTree;
    public  Dictionary<char, string> HuffmanDictionary;


    public HuffmanCodingOutputStep()
    {
        this.LetterAppearances = new();
        this.MinHeap = new();
        this.HuffmanDictionary = new();
    }
}