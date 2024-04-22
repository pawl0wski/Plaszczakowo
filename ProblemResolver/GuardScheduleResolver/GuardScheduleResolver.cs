namespace Problem.GuardSchedule;

public class GuardScheduleResolver :
    ProblemResolver<GuardScheduleInputData, GuardScheduleOutputStep>
{
    public override List<GuardScheduleOutputStep> Resolve(GuardScheduleInputData data)
    {
        List<GuardScheduleOutputStep> outputStep = new();

        IteratePath(data.Plaszczaki, data.Pathway);

        return outputStep;
    }

    private static void IteratePath(List<Plaszczak> plaszczaki, Pathway path)
    {
        List<GuardScheduleOutputStep> outputStep = new();
        int plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {

            if (p.IsGuard(path.MaxVertexValue) == false)
            {
                break;
            }
            
            for (int vertexIndex = 1; vertexIndex < path.Vertices.Count - 1; vertexIndex++)
            {
                UpdatePosition(p, path, vertexIndex);

                MoveForward(p);

                EnoughEnergyOrSteps(p, path.MaxPossibleSteps);

                Resting(p);

                p.Index = plaszczakIndex;
                p.CurrentVertexIndex = vertexIndex;

                GuardScheduleOutputStep plaszczakOutput = new GuardScheduleOutputStep(p);
                outputStep.Add(plaszczakOutput);
            }

            plaszczakIndex++;
        }
    }
    private static void UpdatePosition(Plaszczak p, Pathway path, int i)
    {
        p.PreviousVertexValue = path.Vertices[i - 1];
        p.CurrentVertexValue = path.Vertices[i];
        p.NextVertexValue = path.Vertices[i + 1];
    }
    private static void MoveForward(Plaszczak p)
    {
        p.Energy -= p.CurrentVertexValue;
        p.Steps++;
    }
    private static void EnoughEnergyOrSteps(Plaszczak p, int maxSteps)
    {
        if (p.Energy < p.NextVertexValue || p.Steps == maxSteps)
        {
            p.Steps = 0;

            if (p.CurrentVertexValue > p.PreviousVertexValue)
            {
                ListenMelody(p);
            }
        }
    }
    private static void ListenMelody(Plaszczak p)
    {
        p.Energy = p.MaxEnergy;
        p.Melody++;
    }
    private static void Resting(Plaszczak p)
    {
        if (p.CurrentVertexValue <= p.PreviousVertexValue)
        {
            p.Steps = 0;
            p.Energy = p.MaxEnergy;
        }
    }
}