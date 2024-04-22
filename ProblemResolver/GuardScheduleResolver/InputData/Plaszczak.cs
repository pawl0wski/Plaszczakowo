namespace ProblemResolver.GuardScheduleResolver.InputData;

public class Plaszczak : IComparable<Plaszczak>
{
    public int Energy;
    public int MaxEnergy;
    public int Melody;
    public int Steps;

    public int PreviousVertexValue;
    public int CurrentVertexValue;
    public int NextVertexValue;

    public int CurrentVertexIndex;
    public int Index;

    private static Random Random = new Random();

    public Plaszczak(int energyMax)
    {
        int RandomEnergy = Random.Next(0, energyMax + 1);
        MaxEnergy = RandomEnergy;
        Energy = RandomEnergy;
        Melody = 0;
        Steps = 0;
    }
    public int CompareTo(Plaszczak? other)
    {
        if (other == null)
        {
            return 0;
        }

        return Energy.CompareTo(other.Energy);
    }
    public bool IsGuard(int maxEnergy)
    {
        if (MaxEnergy >= maxEnergy)
        {
            return true;
        }
        return false;
    }
}
