namespace GuardSchedule;

public class Pathway
{
    public List<int> Edges;

    public Pathway()
    {
        this.Edges = new List<int>();
    }

    public static Pathway CreatePathway()
    {
        Pathway path = new();
        Console.Write("How many vertices? ");
        int verticesAmount = int.Parse(Console.ReadLine() ?? "0");
        if (verticesAmount <= 0)
        {
            Console.WriteLine("Wrong input!");
            Environment.Exit(0);
        }

        Console.Write("Edges: ");
        int vertices;
        int lastVertex = 0;
        for (int i = 0; i < verticesAmount; i++)
        {
            vertices = int.Parse(Console.ReadLine() ?? "0");
            if (i == 0)
            {
                lastVertex = vertices;
            }
            path.AddEdge(vertices);
        }
        path.AddEdge(lastVertex);

        return path;
    }
    public void AddEdge(int new_e)
    {
        Edges.Add(new_e);
    }
    public void DisplayPath()
    {
        for (int i = 0; i < Edges.Count - 1; i++)
        {
            Console.WriteLine($"{Edges[i]} -> {Edges[i + 1]}");
        }
    }
    public int MaxVertice()
    {
        int maxVertices = Edges.Max();
        return maxVertices;
    }
}
