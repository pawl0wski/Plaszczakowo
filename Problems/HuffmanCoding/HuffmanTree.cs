using Drawer.GraphDrawer;

namespace Problem.HuffmanCoding;

using Microsoft.Extensions.Configuration.CommandLine;
using ProblemResolver;
using ProblemVisualizer.Commands;
public class HuffmanTree
{
    public List<Node> MinHeap = new();
    public string result = "";

    public List<Node> Nodes = new();

    public void GenerateDictionary(Node? root, string code, ref Dictionary<char, string> dict)
    {
        if (root == null)
        {
            return;
        }
        if (root.IfConnector == false)
        {
            dict.Add(root.Character, code);
            Tuple<char, string> newCode = new(root.Character, code);
        }
        else
        {
            GenerateDictionary(root.Left, code + "0", ref dict);
            GenerateDictionary(root.Right, code + "1", ref dict);
        }
    }

    public void PrintTree(Node? root, string code)
    {
        
        if (root == null)
        {
            return;
        }
        string message = root.ToString();
        if (code.Length == 0)
        {
            message += " Root ";
        }
        else if (code.Last() == '0')
        {
            message += " Left ";
        }
        else
        {
            message += " Right ";
        }
        message += code;
        Console.WriteLine(message);
        if (root.IfConnector == true) {
            PrintTree(root.Left, code + "0");
            PrintTree(root.Right, code + "1");
        }
    }

    public void CreateHuffmanTree(HuffmanCodingInputData input, Dictionary<char, int> letters, ref HuffmanCodingOutput output, ref ProblemRecreationCommands<GraphData> commands)
    {
        GenerateMinHeap(letters);

        output.HuffmanTree = ProccessMinHeap(ref commands);

        commands.Add(new AddNewTextCommand(0, 50, "Wynik:", null));
        commands.Add(new AddNewTextCommand(0, 100, "", null));
        commands.NextStep();
        Dictionary <char, string> dict = new();
        GenerateDictionary(output.HuffmanTree, "", ref dict);
        CodePhrase(dict, input.InputPhrase, ref commands);
    }

    private void GenerateMinHeap(Dictionary<char, int> letters)
    {
        var arrayOfAllKeys = letters.Keys.ToArray();
        for (int i = 0; i < arrayOfAllKeys.Length; i++)
        {
            MinHeap.Add(new Node(arrayOfAllKeys[i], letters[arrayOfAllKeys[i]], false));
        } 
        MinHeap.Sort();
    }

    private Node ProccessMinHeap(ref ProblemRecreationCommands<GraphData> commands)
    {
        Node left, right, top;
        while (MinHeap.Count > 1)
        {
            left = MinHeap.First();
            MinHeap.RemoveAt(0);
            right = MinHeap.First();
            MinHeap.RemoveAt(0);
            top = new Node('%', left.Value + right.Value, true);
            top.Left = left;
            top.Right = right;
            commands.Add(new ClearGraphCommand());
            DrawVertices(top, ref commands);
            GetEdges(top, ref commands);
            commands.NextStep();
            MinHeap.Add(top);
            MinHeap.Sort();
        }
        return MinHeap.First();
    }

    private void DrawVertices(Node? root, ref ProblemRecreationCommands<GraphData> commands, int X = 500, int Y = 200)
    {
        if (root == null)
        {
            return;
        }
        commands.Add(new AddNewVertexCommand(X, Y, root.Character, null));
        
        DrawVertices(root.Left, ref commands, X-80, Y+50);
        DrawVertices(root.Right, ref commands,  X+80, Y+50);
    }
    private void GetEdges(Node? root, ref ProblemRecreationCommands<GraphData> commands)
    {
        Nodes.Clear();
        List<Tuple<int, int>> verticeEdges = new();
        MakeNodeList(root);
        CalculateEdges(ref verticeEdges);
        DrawEdges(verticeEdges, ref commands);
        Console.WriteLine("-------------------");
    }

    private void DrawEdges(List<Tuple<int, int>> verticeEdges, ref ProblemRecreationCommands<GraphData> commands)
    {
        foreach (var edge in verticeEdges)
        {
            Console.WriteLine(edge.Item1 + " | " + edge.Item2);
            commands.Add(new ConnectVertexCommand(edge.Item1, edge.Item2));
        }
    }
    private void MakeNodeList(Node? root)
    {
        if (root == null)
        {
            return;
        }
        if (root.Left != null) { Nodes.Add(root.Left); }
        if (root.Right != null) { Nodes.Add(root.Right); }
        
        MakeNodeList(root.Left);
        MakeNodeList(root.Right);
    }

    private void CalculateEdges(ref List<Tuple<int, int>> verticeEdges)
    {
        for (int i = 0; i<Nodes.Count; i++)
        {
            if (i*2+1 < Nodes.Count)
            {
                Tuple<int, int> edge = new(i, i*2+1);
                verticeEdges.Add(edge);
            }
            if (i*2+2 < Nodes.Count)
            {
                Tuple<int, int> edge = new(i, i*2+2);
                verticeEdges.Add(edge);
            }
        }
    }
    public void CodePhrase(Dictionary<char, string> codes, string text, ref ProblemRecreationCommands<GraphData> commands)
    {
        foreach (char letter in text)
        {
            result += codes[letter];
            commands.Add(new ChangeTextCommand(1, result, 0, 100));
            commands.NextStep();
        }
        

    }
}

