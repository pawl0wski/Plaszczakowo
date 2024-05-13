namespace Problem.HuffmanCoding;

public record HuffmanNode(int Id, int? ConnectTo, int LeftOffset, Node InnerNode)
{
    public readonly int Id = Id;
    public readonly int? ConnectTo = ConnectTo;
    public readonly int LeftOffset = LeftOffset;
    public readonly Node InnerNode = InnerNode;
}

