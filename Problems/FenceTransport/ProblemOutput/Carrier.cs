namespace Problem.FenceTransport;

public record Carrier (int Id, int? CurrentVertexId = 0) {
    public int Id { get; set; } = Id;
    public int? CurrentVertexId { get; set; } = CurrentVertexId;
    public int CarriedValue { get; set; } = 100;
}