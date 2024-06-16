using Plaszczakowo.Drawer.TextReplaceDrawer;
using Plaszczakowo.Drawer.TextReplaceDrawer.States;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.Problems.PhraseCorrection.Output;
using Plaszczakowo.ProblemVisualizer.Commands;

namespace Plaszczakowo.Problems.PhraseCorrection;

public class PhraseCorrection(string phrase)
{
    Dictionary<char, char> _correctLetters = new Dictionary<char, char>()
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

    private string _result = "", _phrase = phrase;

    public void FixPhrase(string inputPhrase, ref PhraseCorrectionOutput output, ref ProblemRecreationCommands<TextReplaceData> commands)
    {
        
        _phrase = inputPhrase;
        for (var i = 0; i < _phrase.Length; i++)
        {
            commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Highlighted));
            commands.NextStep();
            if (_correctLetters.ContainsKey(_phrase[i]))
            {
                commands.Add(new ChangeCharStateCommand(i, TextReplaceStates.Incorrect));
                ChangeAndReplace(i);
                commands.Add(new ChangeCharCommand(i, _correctLetters[_phrase[i]]));
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

        output.FixedPhrase = _result;

    }

    private void ChangeAndReplace(int i)
    {
        _result += _correctLetters[_phrase[i]];
    }

    private void ChangeWithoutReplace(int i)
    {
        _result += _phrase[i];
    }
}

