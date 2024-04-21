using Microsoft.VisualBasic;

namespace Problem.ComputerScienceMachine;

public class ComputerScienceMachineOutputSteps : ProblemOutputSteps
{
    public List<Tuple<char, char>> fixingPhrase;
    public string fixedPhrase;

    public Dictionary<char, int> letterAppearances;
    public PriorityQueue<Node, int> minHeap;
    public Node? huffmanTree;
    public  Dictionary<char, string> huffmanDictionary;


    public ComputerScienceMachineOutputSteps()
    {
        this.fixingPhrase = new List<Tuple<char, char>>();
        this.fixedPhrase = "";
        this.letterAppearances = new Dictionary<char, int>();
        this.minHeap = new PriorityQueue<Node, int>();
        this.huffmanDictionary = new Dictionary<char, string>();
    }
}