public class CinemaService : ICinemaService
{
    private List<Cinema> cinemas = new List<Cinema>();
    private int nextCinemaId = 1;
    private int nextRoomId = 1;

    public Cinema GetCinema(int id)
    {
        return cinemas.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Cinema> GetAllCinemas()
    {
        return cinemas;
    }

    public Cinema CreateCinema(Cinema cinema)
    {
        cinema.Id = nextCinemaId++;
        cinemas.Add(cinema);
        return cinema;
    }

    public Cinema UpdateCinema(Cinema cinema)
    {
        var existingCinema = cinemas.FirstOrDefault(c => c.Id == cinema.Id);
        if (existingCinema != null)
        {
            existingCinema.Name = cinema.Name;
        }
        return existingCinema;
    }

    public void DeleteCinema(int id)
    {
        cinemas.RemoveAll(c => c.Id == id);
    }

    public ScreeningRoom AddScreeningRoom(int cinemaId, ScreeningRoom room)
    {
        var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
        if (cinema != null)
        {
            room.Id = nextRoomId++;
            room.CinemaId = cinemaId;
            cinema.ScreeningRooms.Add(room);
            return room;
        }
        return null;
    }

    public void RemoveScreeningRoom(int cinemaId, int roomId)
    {
        var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
        if (cinema != null)
        {
            cinema.ScreeningRooms.RemoveAll(r => r.Id == roomId);
        }
    }
}