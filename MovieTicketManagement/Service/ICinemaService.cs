public interface ICinemaService
{
    Cinema GetCinema(int id);
    IEnumerable<Cinema> GetAllCinemas();
    Cinema CreateCinema(Cinema cinema);
    Cinema UpdateCinema(Cinema cinema);
    void DeleteCinema(int id);
    ScreeningRoom AddScreeningRoom(int cinemaId, ScreeningRoom room);
    void RemoveScreeningRoom(int cinemaId, int roomId);
}