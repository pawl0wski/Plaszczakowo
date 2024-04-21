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
    public void ToStringTree(Node? other = null)
    {
        if (this == null)
        {
            return;
        }
        else if (this.ifConnector == false)
        {
            ToStringTree(left);
            ToStringTree(right);
        }
        else {
            string result;
            result = $"{character} {value} {ifConnector}";
            Console.WriteLine(result);
        }
        

        return;
    }
}
