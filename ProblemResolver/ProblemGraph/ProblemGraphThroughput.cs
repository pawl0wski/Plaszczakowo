using Drawer.GraphDrawer;

namespace ProblemResolver.Graph;

public record ProblemGraphThroughput(int Flow, int Capacity)
{
    public int Flow { get; set; } = Flow;
    public int Capacity { get; set; } = Capacity;

    public static ProblemGraphThroughput FromGraphThroughput(GraphThroughput throughput) {
        return new(throughput.Flow, throughput.Capacity);
    }
}