using GuardSchedule;

namespace Problem.GuardSchedule;

public class GuardScheduleResolver :
    ProblemResolver<GuardScheduleInputData, GuardScheduleOutputStep>
{
    public override List<GuardScheduleOutputStep> Resolve(GuardScheduleInputData data)
    {
        throw new NotImplementedException();
    }

    private static void IteratePath(List<Plaszczak> plaszczaki, int maxVertex, Pathway path, int maxSteps)
    {
        foreach (var p in plaszczaki)
        {
            if (p.IsGuard(maxVertex) == false)
            {
                break;
            }
            
            for (int currentVertex = 0; currentVertex < path.Edges.Count; currentVertex++)
            {
                UpdatePosition(p, path, currentVertex);

                MoveForward(p);

                EnoughEnergyOrSteps(p, maxSteps);

                Resting(p);
            }
        }
    }
    private static void UpdatePosition(Plaszczak p, Pathway path, int currentVertex)
    {
        if (currentVertex == 0)
        {
            p.PreviousStep = -1;
            p.CurrentStep = path.Edges[currentVertex];
            p.NextStep = path.Edges[currentVertex + 1];
        } 
        else if (currentVertex == path.Edges.Count() - 1)
        {
            p.PreviousStep = path.Edges[currentVertex - 1];
            p.CurrentStep = path.Edges[currentVertex];
            p.NextStep = -1;
        } 
        else
        {
            p.PreviousStep = path.Edges[currentVertex - 1];
            p.CurrentStep = path.Edges[currentVertex];
            p.NextStep = path.Edges[currentVertex + 1];
        }

    }
    private static void MoveForward(Plaszczak p)
    {
        p.Energy -= p.CurrentStep;
        p.Steps++;
    }
    private static void EnoughEnergyOrSteps(Plaszczak p, int maxSteps)
    {
        if (p.Energy < p.NextStep || p.Steps == maxSteps)
        {
            p.Steps = 0;

            if (p.CurrentStep > p.PreviousStep)
            {
                ListenMelody(p);
            }
        }
    }
    private static void ListenMelody(Plaszczak p)
    {
        p.Energy = p.EnergyMax;
        p.Melody++;
    }
    private static void Resting(Plaszczak p)
    {
        if (p.CurrentStep <= p.PreviousStep)
        {
            p.Steps = 0;
            p.Energy = p.EnergyMax;
        }
    }
}