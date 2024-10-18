public class ScreeningRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }


}