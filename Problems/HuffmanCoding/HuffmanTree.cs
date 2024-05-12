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
        int howManyVertices)
    {
        var level = 0;
        var howManyLevels = Convert.ToInt32(Math.Floor(Math.Log2(howManyVertices))) + 1;
        Console.WriteLine("Vert" + howManyVertices);
        var y = 720 / howManyLevels / 2;
        Queue<Node> currentLevel = new();
        Queue<Node> nextLevel = new();

        currentLevel.Enqueue(root);

        var id = 0;
        while (currentLevel.Count > 0 || nextLevel.Count > 0)
        {
            var vertexSpaceWidth = 1280 / Convert.ToInt32(Math.Pow(2, level));
            var isFirstVertex = true;
            var currentx = 0;
            while (currentLevel.Count > 0)
            {
                var current = currentLevel.Dequeue();
                if (isFirstVertex)
                {
                    currentx += vertexSpaceWidth / 2;
                }
                else
                {
                    currentx += vertexSpaceWidth;
                }
                commands.Add(new AddNewVertexCommand(currentx, y, current.Character, null));
                
                commands.Add(id % 2 == 0
                    ? new ConnectVertexCommand(id, (id - 1) / 2)
                    : new ConnectVertexCommand(id, id / 2));
                
                if (current.Left != null)
                    nextLevel.Enqueue(current.Left);
                if (current.Right != null) 
                    nextLevel.Enqueue(current.Right);
                isFirstVertex = false;
                id++;
            }
            (currentLevel, nextLevel) = (nextLevel, currentLevel);
            level++;
            y += 720 / howManyLevels;
        }

    }
    private int RecalculateVerticies(Node root)
    {
        Stack<Node> stack = new();
        stack.Push(root);
        var count = 0;
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

