using ProblemResolver;

namespace Problem.FenceTransport;

public record FenceTransportOutput : ProblemOutput
{
    public int HoursToBuild { get; set; }
}