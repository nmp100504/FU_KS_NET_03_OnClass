public class Cinema
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ScreeningRoom> ScreeningRooms { get; set; } = new List<ScreeningRoom>();
}