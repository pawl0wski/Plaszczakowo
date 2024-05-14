namespace Problem.FenceTransport;

public class Carrier (int Id, int? Position = 0) {
    public int Id { get; set; } = Id;
    public int? Position { get; set; } = Position;
    public int Load { get; set; } = 100;

    public void MoveTo(int newPosition)
    {
        Position = newPosition;
    }
    public void PickUp(int amout)
    {
        Load += amout % 101;
    }
    public void Deliver(int amout)
    {
        Load -= amout;
        if (Load < 0)
            Load = 0;
    }
}