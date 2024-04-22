using System.Security.Cryptography.X509Certificates;

namespace Problem.HuffmanCoding;

public class Node: IComparable<Node>
{
    public char Character;
    public int Value;
    public Node? Left, Right;
    public bool IfConnector;

    public Node(char character, int value, bool ifConnector)
    {
        this.Character = character;
        this.Value = value;
        this.IfConnector = ifConnector;
    }
    public override string ToString()
    {
        if (this == null)
        {
            return "";
        }
        else {
            string result;
            result = $"{Character} {Value} {IfConnector}";
            Console.WriteLine(result);
            return result;
        }
    }

    public int CompareTo(Node? other)
    {
        if (other == null || this == null)
        {
            return 0;
        }
        if (other == this) 
        {
            return 0;
        }
        if (this.Value > other.Value)
        {
            return 1;
        }
        if (this.Value < other.Value)
        {
            return -1;
        }

        return this.Character.CompareTo(other.Character);

    }


}
