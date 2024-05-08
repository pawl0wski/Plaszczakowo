
using Drawer.GraphDrawer;
using ProblemResolver;
namespace Problem.HuffmanCoding;

public class HuffmanCodingResolver :
    ProblemResolver<HuffmanCodingInputData, HuffmanCodingOutput, GraphData>
{
    public Dictionary<char, int> CalculateAppearances(string phrase)
    {
        Dictionary<char, int> letterAppearances = new();
        for (int i = 0; i < phrase.Length; i++)
        {
            if (letterAppearances.ContainsKey(phrase[i]))
            {
                letterAppearances[phrase[i]]++;
            } else
            {
                letterAppearances.Add(phrase[i], 1);
            }
        }
        return letterAppearances;
    }

    public void GenerateHuffmanTree(HuffmanCodingInputData data, Dictionary<char, int> letterAppearances, ref HuffmanCodingOutput output, ref ProblemRecreationCommands<GraphData> commands)
    {
        HuffmanTree huffmanTree = new HuffmanTree();
        huffmanTree.CreateHuffmanTree(data, letterAppearances, ref output, ref commands);
        GenerateTree(ref output, huffmanTree);
    }

    public void GenerateTree(ref HuffmanCodingOutput output, HuffmanTree huffmanTree)
    {
        huffmanTree.GenerateDictionary(output.HuffmanTree, "", ref output.HuffmanDictionary);
    }
    public override HuffmanCodingOutput Resolve(HuffmanCodingInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {

        HuffmanCodingOutput output = new();
        
        Dictionary<char, int> letterAppearances = CalculateAppearances(data.InputPhrase);

        GenerateHuffmanTree(data, letterAppearances, ref output, ref commands);

        return output;
    }

    


}

