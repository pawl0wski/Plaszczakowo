using Plaszczakowo.ProblemResolver;

namespace Plaszczakowo.Problems.FenceTransport.Output;

public record FenceTransportOutput : ProblemOutput
{
    public int HoursToBuild { get; set; }
}