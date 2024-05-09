using Drawer.GraphDrawer;

namespace Problem.HuffmanCoding;

using ProblemResolver;
using ProblemVisualizer.Commands;
public class HuffmanTree
{
    List <Node> MinHeap = new();
    int test = 0;

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
            test++;
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
            DrawVertices(top, ref commands, RecalculateVerticies(top));
            commands.NextStep();
            MinHeap.Sort();
        }
    }

    private void DrawVertices(Node root,
        ref ProblemRecreationCommands<GraphData> commands,
        int HowManyVertices)
    {
        int level = 0;
        int HowManyLevels = Convert.ToInt32(Math.Floor(Math.Log2(HowManyVertices))) + 1;
        Console.WriteLine("Vert" + HowManyVertices);
        int y = 720 / HowManyLevels / 2;
        Queue<Node> CurrentLevel = new();
        Queue<Node> NextLevel = new();

        CurrentLevel.Enqueue(root);

        int id = 0;
        while (CurrentLevel.Count > 0 || NextLevel.Count > 0)
        {
            var VertexSpaceWidth = 1280 / Convert.ToInt32(Math.Pow(2, level));
            bool isFirstVertex = true;
            int currentx = 0;
            while (CurrentLevel.Count > 0)
            {
                var current = CurrentLevel.Dequeue();
                if (isFirstVertex)
                {
                    currentx += VertexSpaceWidth / 2;
                }
                else
                {
                    currentx += VertexSpaceWidth;
                }
                commands.Add(new AddNewVertexCommand(currentx, y, current.Character, null));
                if (id%2 == 0)
                    commands.Add(new ConnectVertexCommand(id, (id-1)/2));
                else
                    commands.Add(new ConnectVertexCommand(id, id/2));
                if (current.Left != null)
                    NextLevel.Enqueue(current.Left);
                if (current.Right != null) 
                    NextLevel.Enqueue(current.Right);
                isFirstVertex = false;
                id++;
            }
            (CurrentLevel, NextLevel) = (NextLevel, CurrentLevel);
            level++;
            y += 720 / HowManyLevels;
        }

    }
    private int RecalculateVerticies(Node root)
    {
        Stack<Node> stack = new();
        stack.Push(root);
        int count = 0;
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (current.Left != null)
                stack.Push(current.Left);
            if (current.Right != null)
                stack.Push(current.Right);
            count++;
        }
        return count;
    }

}

