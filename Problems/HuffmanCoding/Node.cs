namespace Problem.HuffmanCoding;

public class Node : IComparable<Node>
{
    public char Character;
    public bool IfConnector;
    public Node? Left, Right;
    public int Value;

    public Node(char character, int value, bool ifConnector)
    {
        Character = character;
        Value = value;
        IfConnector = ifConnector;
    }

    public int CompareTo(Node? other)
    {
        if (other == null || this == null) return 0;
        if (other == this) return 0;
        if (Value > other.Value) return 1;
        if (Value < other.Value) return -1;

        return Character.CompareTo(other.Character);
    }

    public override string ToString()
    {
        if (this == null) return "";

        string result;
        result = $"{Character} {Value} {IfConnector}";
        Console.WriteLine(result);
        return result;
    }
}