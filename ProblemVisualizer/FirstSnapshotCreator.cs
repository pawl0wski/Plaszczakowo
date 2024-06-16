namespace Plaszczakowo.ProblemVisualizer;

public abstract class FirstSnapshotCreator<TProblemInputData, TDrawerData>(TProblemInputData inputData)
{
    public readonly TProblemInputData InputData = inputData;

    public abstract TDrawerData CreateFirstSnapshot();
}