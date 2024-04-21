using System.Drawing.Printing;

namespace Problem.ComputerScienceMachine;

public class Node
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
}
