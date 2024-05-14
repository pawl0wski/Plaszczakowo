using Microsoft.AspNetCore.SignalR;
using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingOutput : ProblemOutput
{
    public string Result { get; set; } = "";

    public string InputPhrase { get; set; } = "";
    public Dictionary<char, string> HuffmanDictionary { get; set; } = new();
}

