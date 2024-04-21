using Microsoft.VisualBasic;

namespace Problem.ComputerScienceMachine;

public class ComputerScienceMachineOutputSteps : ProblemOutputSteps
{
    public List<Tuple<char, char>> FixingPhrase;
    public string FixedPhrase;

    public Dictionary<char, int> LetterAppearances;
    public PriorityQueue<Node, int> MinHeap;
    public Node? HuffmanTree;
    public  Dictionary<char, string> HuffmanDictionary;


    public ComputerScienceMachineOutputSteps()
    {
        this.FixingPhrase = new();
        this.FixedPhrase = "";
        this.LetterAppearances = new();
        this.MinHeap = new();
        this.HuffmanDictionary = new();
    }
}