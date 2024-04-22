namespace ProblemDrawer;

using Problem;
using GraphDrawer;

using GraphDrawer;
using Problem;

public abstract class ProblemDrawer<TInputData, TOutputStep>
    where TInputData : ProblemInputData
    where TOutputStep : ProblemOutputStep
{
    protected GraphDrawer Drawer;

    protected readonly List<TOutputStep> Steps;

    protected GraphDrawer GraphDrawer;

    protected int CurrentStep;

    protected ProblemDrawer(List<TOutputStep> steps, GraphDrawer drawer)
    {
        Drawer = drawer;
        Steps = steps;
        CurrentStep = 0;
    }

    public virtual async void Next()
    {
        if (CurrentStep >= Steps.Count - 1)
            return;

        CurrentStep++;
        await ModifyGraphDataByCurrentStep();
    }

    public virtual async void Prev()
    {
        if (CurrentStep <= 0)
            return;

        CurrentStep--;
        await ModifyGraphDataByCurrentStep();
    }

    public virtual async void GoToEnd()
    {
        CurrentStep = Steps.Count - 1;
        await ModifyGraphDataByCurrentStep();
    }

    public virtual async void GoToStart()
    {
        CurrentStep = 0;
        await ModifyGraphDataByCurrentStep();
    }

    protected abstract Task ModifyGraphDataByCurrentStep();

    public abstract void CreateGraphDataFromInputData(TInputData data);
}