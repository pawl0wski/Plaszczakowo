using Problem;

namespace ProblemDrawer;

public abstract class ProblemDrawer<TOutputStep> where TOutputStep : ProblemOutputStep
{
    protected readonly List<TOutputStep> Steps;

    protected int CurrentStep;
    
    protected ProblemDrawer(List<TOutputStep> steps)
    {
        Steps = steps;
        CurrentStep = 0;
    }

    public virtual async void Next()
    {
        if (CurrentStep >= Steps.Count-1)
            return;
        
        CurrentStep++;
        await Draw();
    }

    public virtual async void Prev()
    {
        if (CurrentStep <= 0)
            return;
        
        CurrentStep--;
        await Draw();
    }

    public virtual async void GoToEnd()
    {
        CurrentStep = Steps.Count - 1;
        await Draw();
    }

    public virtual async void GoToStart()
    {
        CurrentStep = 0;
        await Draw();
    }

    protected abstract Task Draw();
}