namespace Problem.PhraseCorrection;

public class PhraseCorrection
{
    Dictionary<char, char> correctLetters = new Dictionary<char, char>()
    {
    ['p'] = 'b',
    ['q'] = 'd',
    ['y'] = 'h',
    ['m'] = 'w',
    ['t'] = 'f',
    ['u'] = 'n',
    ['g'] = 'a',
    ['j'] = 'r'
    };

    string result, phrase;

    public PhraseCorrection(string phrase)
    {
        this.result = "";
        this.phrase = phrase;
    }

    public void FixPhrase(ref string inputPhrase,ref List<PhraseCorrectionOutputStep> outputSteps)
    {
        for (int i = 0; i<phrase.Length; i++)
        {
            if (correctLetters.ContainsKey(phrase[i]))
            {
                ChangeAndReplace(i, ref outputSteps);
            }
            else
            {
                ChangeWithoutReplace(i, ref outputSteps);
            }
        } 

        PhraseCorrectionOutputStep final = new(result);
        outputSteps.Add(final);

    }

    private void ChangeAndReplace(int i, ref List<PhraseCorrectionOutputStep> outputSteps)
    {
        Tuple<char, char> step = new Tuple<char,char>(phrase[i], correctLetters[phrase[i]]);
        result += correctLetters[phrase[i]];
        PhraseCorrectionOutputStep stepReplace = new(result, step);
        outputSteps.Add(stepReplace);
    }

    private void ChangeWithoutReplace(int i, ref List<PhraseCorrectionOutputStep> outputSteps)
    {
        result += phrase[i];
        PhraseCorrectionOutputStep step = new(result);
        outputSteps.Add(step);
    }
}