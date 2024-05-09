
using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;
namespace Problem.HuffmanCoding;

public class HuffmanCodingResolver :
    ProblemResolver<HuffmanCodingInputData, HuffmanCodingOutput, GraphData>
{

    private Dictionary<char, int> CalculateCharacterAppearances(string input)
    {
        Dictionary<char, int> result = new();

        foreach (var character in input)
        {
            if (!result.ContainsKey(character))
            {
                result.Add(character, 1);
            }
            else
            {
                result[character]++;
            }
        }

        return result;
    }
    private void GenerateHuffmanDictionary(Node? root, ref Dictionary<char, string> huffmanDictionary, string code = "")
    {
        if (root == null)
        {
            return;
        }
        if (!root.IfConnector)
        {
            huffmanDictionary.Add(root.Character, code);
        }
        GenerateHuffmanDictionary(root.Left, ref huffmanDictionary, code+"0");
        GenerateHuffmanDictionary(root.Right, ref huffmanDictionary, code+"1");
    }

    private string GenerateResult(string inputPhrase, Dictionary<char, string> huffmanDictionary, ref ProblemRecreationCommands<GraphData> commands)
    {
        string result = "";
        foreach (var character in inputPhrase)
        {
            result += huffmanDictionary[character];
            commands.Add(new ChangeTextCommand(0, result, 0, 50));
            commands.NextStep();
        }
        return result;
    }
    
    public override HuffmanCodingOutput Resolve(HuffmanCodingInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {

        HuffmanCodingOutput output = new();

        Dictionary<char, int> letterAppearances = CalculateCharacterAppearances(data.InputPhrase);
        HuffmanTree tree = new();
        Node root = tree.GenerateHuffmanTree(letterAppearances, ref commands);

        GenerateHuffmanDictionary(root, ref output.huffmanDictionary);
        output.result = GenerateResult(data.InputPhrase, output.huffmanDictionary, ref commands);
        return output;
    }

    


}

