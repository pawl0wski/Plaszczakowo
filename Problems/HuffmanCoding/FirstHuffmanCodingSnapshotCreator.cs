using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.Problems.HuffmanCoding.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.HuffmanCoding;

public class FirstHuffmanCodingSnapshotCreator(HuffmanCodingInputData inputData)
    : FirstSnapshotCreator<HuffmanCodingInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        var outputData = new GraphData();
        outputData.Texts.Add(new GraphText("", 0, 0, GraphStates.Active));
        return outputData;
    }
}