namespace Problem.HuffmanCoding;

public class HuffmanCodingResolver :
    ProblemResolver<HuffmanCodingInputData, HuffmanCodingOutputStep>
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

    public void GenerateHuffmanTree(Dictionary<char, int> letterAppearances, ref List<HuffmanCodingOutputStep> outputSteps)
    {
        HuffmanTree huffmanTree = new HuffmanTree();
        Dictionary<char, string> dict = new Dictionary<char, string>();
        huffmanTree.CreateHuffmanTree(letterAppearances, ref outputSteps);
        GenerateTree(ref outputSteps, huffmanTree);
    }

    public void GenerateTree(ref List<HuffmanCodingOutputStep> output, HuffmanTree huffmanTree)
    {
        huffmanTree.GenerateDictionary(output[output.Count-1].HuffmanTree, "", output[0].HuffmanDictionary, ref output);
    }
    public override List<HuffmanCodingOutputStep> Resolve(HuffmanCodingInputData data)
    {
        List<HuffmanCodingOutputStep> outputSteps = new List<HuffmanCodingOutputStep>();

        Dictionary<char, int> letterAppearances = new Dictionary<char, int>();
        
        letterAppearances = CalculateAppearances(data.InputPhrase);

        GenerateHuffmanTree(letterAppearances, ref outputSteps);

        return outputSteps;
    }

    


}