using System.Text.Json.Serialization;

namespace Plaszczakowo.Problems.GuardSchedule.Input;

public record Plaszczak : IComparable<Plaszczak>
{
    private static readonly Random Random = new();

    public Plaszczak(int energyMax)
    {
        var randomEnergy = Random.Next(0, energyMax + 1);
        MaxEnergy = randomEnergy;
        Energy = randomEnergy;
        Melody = 0;
        Steps = 0;
    }

    public Plaszczak(int energy, int melody = 0, int steps = 0)
    {
        MaxEnergy = energy;
        Energy = energy;
        Melody = melody;
        Steps = steps;
    }

    [JsonConstructor]
    public Plaszczak(int CurrentVertexIndex, int CurrentVertexValue, int Energy, int Index,
        int MaxEnergy, int Melody, int NextVertexValue, int PreviousVertexValue, int Steps)
    {
        this.CurrentVertexIndex = CurrentVertexIndex;
        this.CurrentVertexValue = CurrentVertexValue;
        this.Energy = Energy;
        this.Index = Index;
        this.MaxEnergy = MaxEnergy;
        this.Melody = Melody;
        this.NextVertexValue = NextVertexValue;
        this.PreviousVertexValue = PreviousVertexValue;
        this.Steps = Steps;
    }

    public int CurrentVertexIndex { get; set; }
    public int CurrentVertexValue { get; set; }
    public int Energy { get; set; }
    public int Index { get; set; }
    public int MaxEnergy { get; set; }
    public int Melody { get; set; }
    public int NextVertexValue { get; set; }

    public int PreviousVertexValue { get; set; }
    public int Steps { get; set; }

    public int CompareTo(Plaszczak? other)
    {
        if (other == null)
            return 0;
        if (other.Energy > Energy)
            return 1;
        return -1;
    }

    public bool IsGuard(int maxEnergy)
    {
        return MaxEnergy >= maxEnergy;
    }
}