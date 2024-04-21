namespace Problem.ComputerScienceMachine;

public class ComputerScienceMachineResolver :
    ProblemResolver<ComputerScienceMachineInputData, ComputerScienceMachineOutputStep>
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

    public void GenerateHuffmanTree(Dictionary<char, int> letterAppearances, ref List<ComputerScienceMachineOutputStep> outputSteps)
    {
        HuffmanTree huffmanTree = new HuffmanTree();
        Dictionary<char, string> dict = new Dictionary<char, string>();
        Node root = huffmanTree.CreateHuffmanTree(letterAppearances, ref outputSteps);
        outputSteps[0].HuffmanTree = root;
        GenerateTree(ref outputSteps, huffmanTree);
    }

    public void GenerateTree(ref List<ComputerScienceMachineOutputStep> output, HuffmanTree huffmanTree)
    {
        huffmanTree.GenerateDictionary(output[0].HuffmanTree, "", output[0].HuffmanDictionary);
    }
    public override List<ComputerScienceMachineOutputStep> Resolve(ComputerScienceMachineInputData data)
    {
        PhraseCorrection correction = new PhraseCorrection(data.InputPhrase);
        List<ComputerScienceMachineOutputStep> outputSteps = new List<ComputerScienceMachineOutputStep>();
        ComputerScienceMachineOutputStep computerScienceMachineOutputStep = new ComputerScienceMachineOutputStep();
        outputSteps.Add(computerScienceMachineOutputStep);
        string fixedPhrase = "";
        correction.FixPhrase(ref fixedPhrase, ref outputSteps);

        Dictionary<char, int> letterAppearances = new Dictionary<char, int>();
        
        outputSteps[0].LetterAppearances = CalculateAppearances(fixedPhrase);

        GenerateHuffmanTree(outputSteps[0].LetterAppearances, ref outputSteps);

        return outputSteps;
    }

    


}