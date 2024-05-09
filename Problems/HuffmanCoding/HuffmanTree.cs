using Drawer.GraphDrawer;

namespace Problem.HuffmanCoding;

using ProblemResolver;
using ProblemVisualizer.Commands;
public class HuffmanTree
{
    List <Node> MinHeap = new();
    private int currentId = -1;

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
            commands.NextStep();
            currentId = -1;
            MinHeap.Sort();
        }
    }

    private void DrawVertices(Node? root,
        ref ProblemRecreationCommands<GraphData> commands,
        int x = 600,
        int y = 200,
        int level = 0,
        int? parentId = null)
    {
        if (root == null)
        {
            return;
        }
        var id = ++currentId;

        commands.Add(new AddNewVertexCommand(x, y, root.Character, null));
        if (parentId != null)
        {
            commands.Add(new ConnectVertexCommand(parentId ?? 0, currentId));
        }
        if (level == 0)
        {
            DrawVertices(root.Right, ref commands, x+205, y+60, level+1, id);
            DrawVertices(root.Left, ref commands, x-195, y+60, level+1, id);
        }
        else
        {
            DrawVertices(root.Right, ref commands, x+(100+level*10), y+60, level+1, id);
            DrawVertices(root.Left, ref commands, x-(80-level*12), y+60, level+1, id);
        }
    }

}

