namespace Problem.HuffmanCoding;

public class HuffmanTree
{
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
        Node left, right, top;
        var MinHeap = new List<Node>();
        var arrayOfAllKeys = letters.Keys.ToArray();
        var arrayOfAllValues = letters.Values.ToArray();
        for (int i = 0; i < arrayOfAllKeys.Length; i++)
        {
            MinHeap.Add(new Node(arrayOfAllKeys[i], arrayOfAllValues[i], false));
            outputSteps[0].MinHeap.Add(new Node(arrayOfAllKeys[i], arrayOfAllValues[i], false));
        }

        MinHeap.Sort();

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

        return MinHeap.First();
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