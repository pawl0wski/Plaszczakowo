
using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;
namespace Problem.HuffmanCoding;

public class HuffmanCodingResolver :
    ProblemResolver<HuffmanCodingInputData, HuffmanCodingOutput, GraphData>
{

    Dictionary<char, string> _huffmanDictionary = new();
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
    private void GenerateHuffmanDictionary(Node? root, string code = "")
    {
        if (root == null)
        {
            return;
        }
        if (!root.IfConnector)
        {
            _huffmanDictionary.Add(root.Character, code);
        }
        GenerateHuffmanDictionary(root.Left, code+"0");
        GenerateHuffmanDictionary(root.Right, code+"1");
    }

    private static string GenerateResult(string inputPhrase, Dictionary<char, string> huffmanDictionary, ref ProblemRecreationCommands<GraphData> commands)
    {
        var result = "";
        foreach (var character in inputPhrase)
        {
            result += huffmanDictionary[character];
            //commands.Add(new ChangeTextCommand(0, result, 0, 50));
            //commands.NextStep();
        }
        return result;
    }
    
    public override HuffmanCodingOutput Resolve(HuffmanCodingInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {

        HuffmanCodingOutput output = new()
        {
            InputPhrase = data.InputPhrase
        };
        var letterAppearances = CalculateCharacterAppearances(data.InputPhrase);
        HuffmanTree tree = new();
        var root = tree.GenerateHuffmanTree(letterAppearances, ref commands);
        GenerateHuffmanDictionary(root);
        output.HuffmanDictionary = _huffmanDictionary;
        output.Result = GenerateResult(data.InputPhrase, output.HuffmanDictionary, ref commands);
        return output;
    }

    


}

