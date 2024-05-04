using Drawer.TextReplaceDrawer;
using Drawer.TextReplaceDrawer.States;
using ProblemResolver;
using ProblemVisualizer.Commands;
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

    public void FixPhrase(string inputPhrase, ref PhraseCorrectionOutput output, ref ProblemRecreationCommands<TextReplaceData> commands)
    {
        
        phrase = inputPhrase;
        for (var i = 0; i < phrase.Length; i++)
        {
            commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Highlighted));
            commands.NextStep();
            if (correctLetters.ContainsKey(phrase[i]))
            {
                commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Incorrect));
                ChangeAndReplace(i);
                commands.Add(new ChangeCharCommand(i, correctLetters[phrase[i]]));
                commands.NextStep();
                commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Corrected));
                commands.NextStep();
            }
            else
            {
                ChangeWithoutReplace(i);
                commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Inactive));
            }
            commands.Add(new MoveTextToRightCommand());
        } 

        output.FixedPhrase = result;

    }

    private void ChangeAndReplace(int i)
    {
        result += correctLetters[phrase[i]];
    }

    private void ChangeWithoutReplace(int i)
    {
        result += phrase[i];
    }
}

