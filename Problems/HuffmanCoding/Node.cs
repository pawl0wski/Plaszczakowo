namespace Plaszczakowo.Problems.HuffmanCoding;

public class Node : IComparable<Node>
{
    public readonly char Character;
    public readonly bool IfConnector;
    public readonly int Value;
    public int? ConnectTo;
    public int Id;
    public Node? Left, Right;
    public int LeftOffset;

    public Node(char character, int value, bool ifConnector)
    {
        Character = character;
        Value = value;
        IfConnector = ifConnector;
    }

    public int CompareTo(Node? other)
    {
        if (other == null) return 0;
        if (other == this) return 0;
        if (Value > other.Value) return 1;
        if (Value < other.Value) return -1;

        return Character.CompareTo(other.Character);
    }

    public Node InsertAdditionalData(int id, int? connectTo, int leftOffset)
    {
        Id = id;
        ConnectTo = connectTo;
        LeftOffset = leftOffset;
        return this;
    }

    public override string ToString()
    {
        return $"{Character} {Value} {IfConnector}";
    }
}