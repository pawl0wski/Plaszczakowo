using System.Drawing.Printing;

namespace Problem.ComputerScienceMachine;

public class Node
{
    public char character;
    public int value;
    public Node? left, right;
    public bool ifConnector;

    public Node(char character, int value, bool ifConnector)
    {
        this.character = character;
        this.value = value;
        this.ifConnector = ifConnector;
    }
    public void ToString(Node? other = null)
    {
        string result;
        result = character + " " + " " + value + " " + ifConnector;
        if (this.left != null)
        {
            ToString(left);
        }
        if (this.right != null)
        {
            ToString(right);
        }
        
    }
}

public class NodeComparer : IComparer<Node>
{
    public int Compare(Node? one, Node? two)
    {
        if (one == null || two == null)
        {
            return 0;
        }
        return one.value.CompareTo(two.value);
    }
}
