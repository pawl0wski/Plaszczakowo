using Drawer.GraphDrawer;

namespace Problem.HuffmanCoding;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;
public class HuffmanTree
{
    public List<Node> MinHeap = new List<Node>();
    public string result = "";


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

    public void CreateHuffmanTree(Dictionary<char, int> letters, ref HuffmanCodingOutput output, ref ProblemRecreationCommands<GraphData> commands)
    {
        GenerateMinHeap(letters);

        output.HuffmanTree = ProccessMinHeap(ref commands);

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
            int id = 0;
            drawTree(top, ref commands, ref id);
            commands.NextStep();
            MinHeap.Add(top);
            MinHeap.Sort();
        }
        return MinHeap.First();
    }

    private void drawTree(Node? root, ref ProblemRecreationCommands<GraphData> commands, ref int id, int X = 500, int Y = 200)
    {
        if (root == null)
        {
            return;
        }
        commands.Add(new AddNewVertexCommand(X, Y, root.Character, null));
        if (id != 0)
        {
            commands.Add(new ConnectVertexCommand(id-1, id));
        }
        id += 1;
        drawTree(root.Left, ref commands, ref id, X-150, Y+50);
        id += 1;
        drawTree(root.Right, ref commands, ref id, X+150, Y+50);
        return;
    }
    
    public void CodePhrase(Dictionary<char, string> codes, string text)
    {
        foreach (char letter in text)
        {
            result += codes[letter];
            result += "|";
        }
    }
}

