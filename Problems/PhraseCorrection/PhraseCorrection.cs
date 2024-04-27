// namespace Problem.PhraseCorrection;
//
// public class PhraseCorrection
// {
//     Dictionary<char, char> correctLetters = new Dictionary<char, char>()
//     {
//     ['p'] = 'b',
//     ['q'] = 'd',
//     ['y'] = 'h',
//     ['m'] = 'w',
//     ['t'] = 'f',
//     ['u'] = 'n',
//     ['g'] = 'a',
//     ['j'] = 'r'
//     };
//
//     string result, phrase;
//     PhraseCorrectionOutput replaceSteps;
//
//     public PhraseCorrection(string phrase)
//     {
//         this.result = "";
//         this.phrase = phrase;
//         this.replaceSteps = new();
//     }
//
//     public void FixPhrase(ref string inputPhrase,ref List<PhraseCorrectionOutput> outputSteps)
//     {
//         for (int i = 0; i<phrase.Length; i++)
//         {
//             if (correctLetters.ContainsKey(phrase[i]))
//             {
//                 ChangeAndReplace(i);
//             }
//             else
//             {
//                 ChangeWithoutReplace(i);
//             }
//         } 
//
//
//         inputPhrase = result;
//         outputSteps[0].FixedPhrase = result;
//         outputSteps[0].FixingPhrase = replaceSteps.FixingPhrase;
//     }
//
//     private void ChangeAndReplace(int i)
//     {
//         Tuple<char, char> step = new Tuple<char,char>(phrase[i], correctLetters[phrase[i]]);
//         result += correctLetters[phrase[i]];
//         replaceSteps.FixingPhrase.Add(step);
//     }
//
//     private void ChangeWithoutReplace(int i)
//     {
//         result += phrase[i];
//     }
// }