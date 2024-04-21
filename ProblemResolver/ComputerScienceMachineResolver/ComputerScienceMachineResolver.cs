namespace Problem.ComputerScienceMachine;

public class ComputerScienceMachineResolver :
    ProblemResolver<ComputerScienceMachineInputData, ComputerScienceMachineOutputSteps>
{
    public override List<ComputerScienceMachineOutputSteps> Resolve(ComputerScienceMachineInputData data)
    {
        PhraseCorrection correction = new PhraseCorrection();
        List<ComputerScienceMachineOutputSteps> outputSteps = new List<ComputerScienceMachineOutputSteps>();
        ComputerScienceMachineOutputSteps computerScienceMachineOutputStep = new ComputerScienceMachineOutputSteps();
        outputSteps.Add(computerScienceMachineOutputStep);
        string fixedPhrase = data.inputPhrase;
        correction.fixPhrase(ref fixedPhrase, ref outputSteps);
        Dictionary<char, int> letterAppearances = new Dictionary<char, int>();
        for (int i = 0; i < fixedPhrase.Length; i++)
        {
            if (letterAppearances.ContainsKey(fixedPhrase[i]))
            {
                letterAppearances[fixedPhrase[i]]++;
            } else
            {
                letterAppearances.Add(fixedPhrase[i], 1);
            }
        }
        outputSteps[0].letterAppearances = letterAppearances;

        HuffmanTree huffmanTree = new HuffmanTree();

        Dictionary<char, string> dict = new Dictionary<char, string>();

        Node root = huffmanTree.CreateHuffmanTree(letterAppearances, ref outputSteps);

        outputSteps[0].huffmanTree = root;

        huffmanTree.GenerateDictionary(root, "", dict);

        outputSteps[0].huffmanDictionary = dict;

        List<string> codedPhrase = new List<string>();

        return outputSteps;
    }

    


}