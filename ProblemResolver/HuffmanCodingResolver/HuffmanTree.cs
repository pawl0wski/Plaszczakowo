namespace Problem.HuffmanCoding;

public class HuffmanTree
{
    public List<Node> MinHeap = new List<Node>();

    public void GenerateDictionary(Node? root, string code, Dictionary<char, string> dict, ref List<HuffmanCodingOutputStep> outputSteps)
    {
        if (root == null)
        {
            return;
        }
        if (root.IfConnector == false)
        {
            dict.Add(root.Character, code);
            Tuple<char, string> newCode = new(root.Character, code);
            HuffmanCodingOutputStep step = new(null, newCode);
            outputSteps.Add(step);
        }
        else
        {
            GenerateDictionary(root.Left, code + "0", dict, ref outputSteps);
            GenerateDictionary(root.Right, code + "1", dict, ref outputSteps);
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

    public void CreateHuffmanTree(Dictionary<char, int> letters, ref List<HuffmanCodingOutputStep> outputSteps)
    {
        GenerateMinHeap(letters);

        ProccessMinHeap(ref outputSteps);

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

    private void ProccessMinHeap(ref List<HuffmanCodingOutputStep> outputSteps)
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
            HuffmanCodingOutputStep step = new(top, null);
            outputSteps.Add(step);
            MinHeap.Add(top);
            MinHeap.Sort();
        }
    }

    public void HuffmanCode(Dictionary<char, string> codes, string text)
    {
        string result = "";
        foreach (char letter in text)
        {
            result += codes[letter];
            result += "|";
        }
    }
}