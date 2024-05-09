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
        int x = 960,
        int y = 200,
        int level = 0,
        int? parentId = null)
    {
        if (root == null)
        {
            return;
        }
        var id = ++currentId;
        
        Stack<Node> CurrentLevel = new();
        Stack<Node> NextLevel = new();
        if (parentId != null)
        {
            CurrentLevel.Push(root);
            //commands.Add(new ConnectVertexCommand(parentId ?? 0, currentId));
        }

        while (CurrentLevel.Count > 0 && NextLevel.Count > 0)
        {
            var VertexSpaceWidth = 1920 / Convert.ToInt32(Math.Pow(2, level+1));
            bool isFirstVertex = true;
            int currentx = 0;
            while (CurrentLevel.Count > 0)
            {
                var current = CurrentLevel.Pop();
                if (isFirstVertex)
                    currentx += VertexSpaceWidth / 2;
                else
                    currentx += VertexSpaceWidth;
                commands.Add(new AddNewVertexCommand(currentx, y + (level * 60), current.Character, null));
                //commands.Add(new ConnectVertexCommand(id, ++currentId));
                if (current.Left != null)
                    NextLevel.Push(current.Left);
                    Console.WriteLine("Left");
                if (current.Right != null) 
                    NextLevel.Push(current.Right);
                    Console.WriteLine("Right");
                isFirstVertex = false;
            }
            (CurrentLevel, NextLevel) = (NextLevel, CurrentLevel);
            level++;
        }

    }

}

