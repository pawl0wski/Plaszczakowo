using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemVisualizer.Commands;

namespace Plaszczakowo.Problems.HuffmanCoding;

public class HuffmanTree
{
    private const int ScreenWidth = 1280;
    private const int ScreenHeight = 720;
    private readonly List<Node> _minHeap = new();

    public Node GenerateHuffmanTree(Dictionary<char, int> letterAppearances,
        ref ProblemRecreationCommands<GraphData> commands)
    {
        GenerateMinHeap(letterAppearances);
        if (letterAppearances.Count == 1) 
            CreateSingleNodeTree(ref commands);
        else
            GenerateHuffmanHeap(ref commands);
        return _minHeap.First();
    }

    private void GenerateMinHeap(Dictionary<char, int> letterAppearances)
    {
        foreach (var letter in letterAppearances.Keys) _minHeap.Add(new Node(letter, letterAppearances[letter], false));

        _minHeap.Sort();
    }

    private Node MakeConnector()
    {
        var left = _minHeap.First();
        _minHeap.RemoveAt(0);
        var right = _minHeap.First();
        _minHeap.RemoveAt(0);
        Node top = new('%', left.Value + right.Value, true)
        {
            Left = left,
            Right = right
        };
        return top;
    }

    private Node CreateLeftConnector()
    {
        var left = _minHeap.First();
        _minHeap.RemoveAt(0);
        Node top = new('%', left.Value, true)
        {
            Left = left
        };
        return top;
    }

    private void CreateSingleNodeTree(ref ProblemRecreationCommands<GraphData> commands)
    {
        var top = CreateLeftConnector();
        _minHeap.Add(top);
        commands.Add(new ClearGraphCommand());
        DrawVertices(top, ref commands, CalculateLevels(top));
        commands.NextStep();
    }

    private void GenerateHuffmanHeap(ref ProblemRecreationCommands<GraphData> commands)
    {
        while (_minHeap.Count > 1)
        {
            var top = MakeConnector();
            _minHeap.Add(top);
            commands.Add(new ClearGraphCommand());
            DrawVertices(top, ref commands, CalculateLevels(top));
            commands.NextStep();
            _minHeap.Sort();
        }
    }

    private static void DrawVertices(Node root,
        ref ProblemRecreationCommands<GraphData> commands,
        int howManyLevels)
    {
        var level = 0;
        var y = ScreenHeight / howManyLevels / 2;
        Queue<Node> currentLevel = new();
        Queue<Node> nextLevel = new();

        currentLevel.Enqueue(root);

        var id = 0;
        while (currentLevel.Count > 0 || nextLevel.Count > 0)
        {
            var vertexSpaceWidth = ScreenWidth / Convert.ToInt32(Math.Pow(2, level));
            var startX = vertexSpaceWidth / 2;
            while (currentLevel.Count > 0)
            {
                var current = currentLevel.Dequeue();

                commands.Add(new AddNewVertexCommand(startX + vertexSpaceWidth * current.LeftOffset, y,
                    current.Character, GraphStates.Text));

                if (current.ConnectTo is not null)
                {
                    commands.Add(new AddNewEdgeCommand(current.Id, current.ConnectTo ?? 0));
                    commands.Add(new ChangeLastEdgeThroughputCommand(
                        new GraphThroughput(current.Id % 2 == 0 ? 1 : 0)));
                }

                if (current.Left is not null)
                    nextLevel.Enqueue(current.Left.InsertAdditionalData(++id, current.Id, current.LeftOffset * 2));

                if (current.Right is not null)
                    nextLevel.Enqueue(current.Right.InsertAdditionalData(++id, current.Id, current.LeftOffset * 2 + 1));
            }

            (currentLevel, nextLevel) = (nextLevel, currentLevel);
            level++;
            y += ScreenHeight / howManyLevels;
        }
    }

    private static int CalculateLevels(Node root)
    {
        Stack<Node> current = new();
        Stack<Node> next = new();
        current.Push(root);
        var level = 0;
        while (current.Count > 0 || next.Count > 0)
        {
            while (current.Count > 0)
            {
                var node = current.Pop();
                if (node.Left != null)
                    next.Push(node.Left);
                if (node.Right != null)
                    next.Push(node.Right);
            }

            (current, next) = (next, current);
            level++;
        }

        return level;
    }
}