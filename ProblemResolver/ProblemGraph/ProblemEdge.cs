using Drawer.GraphDrawer;

namespace ProblemResolver.Graph;

public record ProblemEdge(int Id, int From, int To)
{
    public int Id { get; set; } = Id;

    public int From { get; set; } = From;
    public int To { get; set; } = To;

}