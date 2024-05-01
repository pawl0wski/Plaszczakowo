// namespace ProblemResolver.GuardSchedule;
//
// public class Plaszczak : IComparable<Plaszczak>
// {
//     private static readonly Random Random = new();
//
//     public int CurrentVertexIndex;
//     public int CurrentVertexValue;
//     public int Energy;
//     public int Index;
//     public int MaxEnergy;
//     public int Melody;
//     public int NextVertexValue;
//
//     public int PreviousVertexValue;
//     public int Steps;
//
//     public Plaszczak(int energyMax)
//     {
//         var RandomEnergy = Random.Next(0, energyMax + 1);
//         MaxEnergy = RandomEnergy;
//         Energy = RandomEnergy;
//         Melody = 0;
//         Steps = 0;
//     }
//
//     public int CompareTo(Plaszczak? other)
//     {
//         if (other == null)
//             return 0;
//         if (other.Energy > Energy)
//             return 1;
//         return -1;
//     }
//
//     public bool IsGuard(int maxEnergy)
//     {
//         if (MaxEnergy >= maxEnergy) return true;
//         return false;
//     }
// }