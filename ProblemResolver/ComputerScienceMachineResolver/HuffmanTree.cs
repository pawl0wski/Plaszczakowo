namespace Problem.ComputerScienceMachine;

public class HuffmanTree
{
    public void GenerateDictionary(Node? root, string code, Dictionary<char, string> dict)
    {
        if (root == null)
        {
            return;
        }
        if (root.ifConnector == false)
        {
            dict.Add(root.character, code);
        }
        else
        {
            GenerateDictionary(root.left, code + "0", dict);
            GenerateDictionary(root.right, code + "1", dict);
        }
    }

    public void PrintTree(Node? root, string code)
    {
        if (root == null)
        {
            return;
        }
        if (root.ifConnector == false)
        {
            Console.WriteLine(root.ToString());
        }
        else
        {
            PrintTree(root.left, code + "0");
            PrintTree(root.right, code + "1");
        }
    }

    public Node CreateHuffmanTree(Dictionary<char, int> letters, ref List<ComputerScienceMachineOutputStep> outputSteps)
    {
        Node left, right, top;
        var MinHeap = new PriorityQueue<Node, int>();
        var arrayOfAllKeys = letters.Keys.ToArray();
        var arrayOfAllValues = letters.Values.ToArray();
        for (int i = 0; i < arrayOfAllKeys.Length; i++)
        {
            MinHeap.Enqueue(new Node(arrayOfAllKeys[i], arrayOfAllValues[i], false), arrayOfAllValues[i]);
            outputSteps[0].MinHeap.Enqueue(new Node(arrayOfAllKeys[i], arrayOfAllValues[i], false), arrayOfAllValues[i]);
        }

        while (MinHeap.Count != 1)
        {
            left = MinHeap.Dequeue();
            right = MinHeap.Dequeue();
            top = new Node('%', left.value + right.value, true);
            top.left = left;
            top.right = right;
            MinHeap.Enqueue(top, top.value);
        }

        return MinHeap.Dequeue();
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