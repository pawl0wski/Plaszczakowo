namespace Problem.HuffmanCoding;

public class HuffmanTree
{
    public List<Node> MinHeap = new List<Node>();

    public void GenerateDictionary(Node? root, string code, Dictionary<char, string> dict)
    {
        if (root == null)
        {
            return;
        }
        if (root.IfConnector == false)
        {
            dict.Add(root.Character, code);
        }
        else
        {
            GenerateDictionary(root.Left, code + "0", dict);
            GenerateDictionary(root.Right, code + "1", dict);
        }
    }

    public void PrintTree(Node? root, string code)
    {
        if (root == null)
        {
            return;
        }
        if (root.IfConnector == false)
        {
            Console.WriteLine(root.ToString());
        }
        else
        {
            PrintTree(root.Left, code + "0");
            PrintTree(root.Right, code + "1");
        }
    }

    public Node CreateHuffmanTree(Dictionary<char, int> letters, ref List<HuffmanCodingOutputStep> outputSteps)
    {
        GenerateMinHeap(letters, ref outputSteps);

        ProccessMinHeap();

        return MinHeap.First();
    }

    private void GenerateMinHeap(Dictionary<char, int> letters, ref List<HuffmanCodingOutputStep> outputSteps)
    {
        var arrayOfAllKeys = letters.Keys.ToArray();
        for (int i = 0; i < arrayOfAllKeys.Length; i++)
        {
            MinHeap.Add(new Node(arrayOfAllKeys[i], letters[arrayOfAllKeys[i]], false));
            outputSteps[0].MinHeap.Add(new Node(arrayOfAllKeys[i], letters[arrayOfAllKeys[i]], false));
        } 
        MinHeap.Sort();
    }

    private void ProccessMinHeap()
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