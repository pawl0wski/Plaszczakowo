namespace GuardSchedule;

internal class Plaszczak : IComparable<Plaszczak>
{
    public int Energy;
    public int EnergyMax;
    public int Melody;
    public int Steps;

    public int PreviousStep;
    public int CurrentStep;
    public int NextStep;

    private static Random Random = new Random();

    public Plaszczak(int energyMax)
    {
        int RandomEnergy = Random.Next(0, energyMax + 1);
        this.EnergyMax = RandomEnergy;
        this.Energy = RandomEnergy;
        this.Melody = 0;
        this.Steps = 0;
    }
    public int CompareTo(Plaszczak? other)
    {
        if (other == null)
        {
            return 0;
        }

        return this.Energy.CompareTo(other.Energy);
    }
    public bool IsGuard(int maxEnergy)
    {
        if (this.EnergyMax >= maxEnergy)
        {
            return true;
        }
        return false;
    }
}
