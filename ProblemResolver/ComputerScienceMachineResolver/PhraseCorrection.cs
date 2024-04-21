using System.Linq.Expressions;

namespace Problem.ComputerScienceMachine;

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

    public void fixPhrase(ref string phrase, ref List<ComputerScienceMachineOutputSteps> outputSteps)
    {
        var arrayOfAllKeys = correctLetters.Keys.ToArray();
        List<Tuple<char, char>> steps = new List<Tuple<char, char>>();
        string result = "";
        for (int i = 0; i<phrase.Length; i++)
        {
            if (correctLetters.ContainsKey(phrase[i]))
            {
                Tuple<char, char> step = new Tuple<char,char>(phrase[i], correctLetters[phrase[i]]);
                result += correctLetters[phrase[i]];
                outputSteps[0].fixingPhrase.Add(step);
            }
            else
            {
                if (phrase[i] == ' ')
                {
                    continue;
                }
                result += phrase[i];
            }
        } 


        phrase = result;
        outputSteps[0].fixedPhrase = result;
        
    }
}