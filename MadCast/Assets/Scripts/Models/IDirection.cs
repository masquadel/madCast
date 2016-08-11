namespace Models
{
    public interface IDirection
    {
        string Arrow { get; }
        int Chance { get; set; }
        float X { get; }
        float Y { get; }
    }
}