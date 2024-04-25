namespace Problem.GuardSchedule;

public class GuardScheduleResolver :
    ProblemResolver<GuardScheduleInputData, GuardScheduleOutputStep>
{
    public override List<GuardScheduleOutputStep> Resolve(GuardScheduleInputData data)
    {
        List<GuardScheduleOutputStep> outputStep = new();

        outputStep = IteratePath(data.Plaszczaki, data.Pathway, outputStep);

        return outputStep;
    }

    private List<GuardScheduleOutputStep> IteratePath(List<Plaszczak> plaszczaki, Pathway path, List<GuardScheduleOutputStep> outputStep)
    {
        plaszczaki.Sort();
        int plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(path.MaxVertexValue) == false)
            {
                break;
            }

            for (int vertexIndex = 0; vertexIndex < path.Vertices.Count; vertexIndex++)
            {
                UpdatePosition(p, path, vertexIndex);

                p.Steps++;

                EnoughEnergyOrSteps(p, path.MaxPossibleSteps);

                Resting(p);

                if (p.NextVertexValue != int.MinValue)
                {
                    p.Energy -= p.NextVertexValue;
                }

                p.CurrentVertexIndex = vertexIndex;
                outputStep.Add(new GuardScheduleOutputStep(p.Index, p.CurrentVertexIndex, p.Energy, p.Melody, p.Steps));
            }
            plaszczakIndex++;
        }

        return outputStep;
    }

    private static void UpdatePosition(Plaszczak p, Pathway path, int vertexIndex)
    {
        if (vertexIndex == 0)
        {
            p.PreviousVertexValue = int.MaxValue;
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = path.Vertices[vertexIndex + 1];
        } 
        else if (vertexIndex == path.Vertices.Count - 1)
        {
            p.PreviousVertexValue = path.Vertices[vertexIndex - 1];
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = int.MinValue;
        }
        else
        {
            p.PreviousVertexValue = path.Vertices[vertexIndex - 1];
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = path.Vertices[vertexIndex + 1];
        }
    }

    private static void EnoughEnergyOrSteps(Plaszczak p, int maxSteps)
    {
        if (p.Energy < p.NextVertexValue || p.Steps == maxSteps)
        {
            if (!(p.CurrentVertexValue < p.PreviousVertexValue))
            {
                ListenMelody(p);
            } 
        }
    }

    private static void ListenMelody(Plaszczak p)
    {
        p.Steps = 0;
        p.Energy = p.MaxEnergy;
        p.Melody++;
    }

    private static void Resting(Plaszczak p)
    {
        if (p.CurrentVertexValue < p.PreviousVertexValue)
        {
            p.Steps = 0;
            p.Energy = p.MaxEnergy;
        }
    }
}