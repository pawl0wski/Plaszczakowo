namespace Plaszczakowo.Problems.HuffmanCoding;

public class Node : IComparable<Node>
{
    public int Id  = 0;
    public int? ConnectTo = null;
    public int LeftOffset = 0;
    public readonly char Character;
    public readonly bool IfConnector;
    public Node? Left, Right;
    public readonly int Value;

    public Node(char character, int value, bool ifConnector)
    {
        Character = character;
        Value = value;
        IfConnector = ifConnector;
    }

    public Node InsertAdditionalData(int id, int? connectTo, int leftOffset)
    {
        Id = id;
        ConnectTo = connectTo;
        LeftOffset = leftOffset;
        return this;
    }
    
    public int CompareTo(Node? other)
    {
        if (other == null) return 0;
        if (other == this) return 0;
        if (Value > other.Value) return 1;
        if (Value < other.Value) return -1;

        return Character.CompareTo(other.Character);
    }

    public override string ToString() => $"{Character} {Value} {IfConnector}";
}