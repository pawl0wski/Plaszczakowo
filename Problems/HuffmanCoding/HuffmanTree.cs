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
        GenerateHuffmanHeap(ref commands);
        return MinHeap.First();
    }

    private void GenerateMinHeap(Dictionary<char, int> letterAppearances)
    {
        foreach(var letter in letterAppearances.Keys)
        {
            MinHeap.Add(new(letter, letterAppearances[letter], false));
        }
        MinHeap.Sort();
    }

    private Node MakeConnector()
    {
        Node left = MinHeap.First();
        MinHeap.RemoveAt(0);
        Node right = MinHeap.First();
        MinHeap.RemoveAt(0);
        Node top = new('%', left.Value + right.Value, true)
        {
            Left = left,
            Right = right
        };
        return top;
    }

    private void GenerateHuffmanHeap(ref ProblemRecreationCommands<GraphData> commands)
    {
        while(MinHeap.Count > 1)
        {
            Node top = MakeConnector();
            MinHeap.Add(top);
            commands.Add(new ClearGraphCommand());
            DrawVertices(top, ref commands);
            MinHeap.Sort();
            commands.NextStep();
        }
    }

    private void DrawVertices(Node? root, ref ProblemRecreationCommands<GraphData> commands, int X = 600, int Y = 200, int level = 0)
    {
        if (root == null)
        {
            return;
        }

        commands.Add(new AddNewVertexCommand(X, Y, root.Character, null));
        if (level == 0)
        {
            DrawVertices(root.Left, ref commands, X-195, Y+60, level+1);
            DrawVertices(root.Right, ref commands, X+205, Y+60, level+1);
        }
        else
        {
            DrawVertices(root.Left, ref commands, X-(80-level*12), Y+60, level+1);
            DrawVertices(root.Right, ref commands, X+(100+level*10), Y+60, level+1);
        }
    }

}

