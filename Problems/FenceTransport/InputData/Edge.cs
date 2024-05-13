namespace Problem.CarrierAssignment;

public record Edge(int From, int To)
{
    public int From { get; set; } = From;
    public int To { get; set; } = To;
    public int Flow { get; set; } = 0;
    public int Capacity { get; set; } = 1;
}