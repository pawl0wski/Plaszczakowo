using Drawer.GraphDrawer;

namespace Problem.HuffmanCoding;

using Microsoft.Extensions.Configuration.CommandLine;
using ProblemResolver;
using ProblemVisualizer.Commands;
public class HuffmanTree
{
    List <Node> MinHeap = new();

    public Node GenerateHuffmanTree(Dictionary<char, int> letterAppearances, ref ProblemRecreationCommands<GraphData> commands)
    {
        GenerateMinHeap(letterAppearances);
        GenerateHuffmanHeap();
        return MinHeap.First();
    }

    private void GenerateMinHeap(Dictionary<char, int> letterAppearances)
    {
        foreach(var letter in letterAppearances.Keys)
        {
            MinHeap.Add(new(letter, letterAppearances[letter], false));
        }
        MinHeap.Sort();
        Console.WriteLine(MinHeap.Count);
    }

    private void GenerateHuffmanHeap()
    {
        while(MinHeap.Count > 1)
        {
            Node left = MinHeap.First();
            MinHeap.RemoveAt(0);
            Node right = MinHeap.First();
            MinHeap.RemoveAt(0);
            Node top = new('%', left.Value+right.Value, true);
            top.Left = left;
            top.Right = right;
            MinHeap.Add(top);
            MinHeap.Sort();
        }
    }

}

