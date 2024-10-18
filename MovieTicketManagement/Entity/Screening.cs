public class Screening
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int ScreeningRoomId { get; set; }
    public ScreeningRoom ScreeningRoom { get; set; }
    public DateTime ShowDateTime { get; set; }

}