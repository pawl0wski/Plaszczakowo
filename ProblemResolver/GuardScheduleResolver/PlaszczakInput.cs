namespace GuardSchedule;

internal class PlaszczakInput
{
    public PlaszczakInput()
    {
        List<Plaszczak> Plaszczaki = new List<Plaszczak>();
    }
    public static List<Plaszczak> GeneratePlaszczaki()
    {
        List<Plaszczak> Plaszczaki = new();

        Console.Write("How many Płaszczaki create? ");
        int plaszczakiAmount = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Maximum Płaszczak energy: ");
        int maxEnergy = int.Parse(Console.ReadLine() ?? "0");

        for (int i = 0; i < plaszczakiAmount; i++)
        {
            Plaszczaki.Add(new Plaszczak(maxEnergy));
        }
        return Plaszczaki;
    }
}
