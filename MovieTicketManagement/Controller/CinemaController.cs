public class CinemaController
{
    private readonly ICinemaService _cinemaService;

    public CinemaController(ICinemaService cinemaService)
    {
        _cinemaService = cinemaService;
    }

    public void CreateCinema()
    {
        Console.Write("Enter cinema name: ");
        string name = Console.ReadLine();
        var cinema = new Cinema { Name = name };
        _cinemaService.CreateCinema(cinema);
        Console.WriteLine("Cinema created successfully.");
    }

    public void UpdateCinema()
    {
        ListCinemas();
        int id;
        do
        {
            Console.Write("Enter cinema ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            var cinema = _cinemaService.GetCinema(id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema not found. Please try again.");
                continue;
            }
            Console.Write("Enter new cinema name: ");
            string name = Console.ReadLine();
            cinema.Name = name;
            _cinemaService.UpdateCinema(cinema);
            Console.WriteLine("Cinema updated successfully.");
            break;
        } while (true);
    }

    public void DeleteCinema()
    {
        ListCinemas();
        int id;
        do
        {
            Console.Write("Enter cinema ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            var cinema = _cinemaService.GetCinema(id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema not found. Please try again.");
                continue;
            }
            _cinemaService.DeleteCinema(id);
            Console.WriteLine("Cinema deleted successfully.");
            break;
        } while (true);
    }

    public void ListCinemas()
    {
        var cinemas = _cinemaService.GetAllCinemas();
        foreach (var cinema in cinemas)
        {
            Console.WriteLine($"ID: {cinema.Id}, Name: {cinema.Name}");
        }
    }

    public void AddScreeningRoom()
    {
        Console.Write("Enter cinema ID: ");
        int cinemaId = int.Parse(Console.ReadLine());
        Console.Write("Enter room name: ");
        string name = Console.ReadLine();
        Console.Write("Enter room capacity: ");
        int capacity = int.Parse(Console.ReadLine());

        var room = new ScreeningRoom { Name = name, Capacity = capacity };
        var addedRoom = _cinemaService.AddScreeningRoom(cinemaId, room);
        if (addedRoom != null)
        {
            Console.WriteLine("Screening room added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add screening room.");
        }
    }

    public void RemoveScreeningRoom()
    {
        ListCinemas();
        int cinemaId;
        do
        {
            Console.Write("Enter cinema ID: ");
            if (!int.TryParse(Console.ReadLine(), out cinemaId))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            var cinema = _cinemaService.GetCinema(cinemaId);
            if (cinema == null)
            {
                Console.WriteLine("Cinema not found. Please try again.");
                continue;
            }
            
            Console.WriteLine("Screening rooms:");
            foreach (var room in cinema.ScreeningRooms)
            {
                Console.WriteLine($"ID: {room.Id}, Name: {room.Name}");
            }

            int roomId;
            Console.Write("Enter room ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out roomId))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            if (!cinema.ScreeningRooms.Any(r => r.Id == roomId))
            {
                Console.WriteLine("Room not found. Please try again.");
                continue;
            }
            _cinemaService.RemoveScreeningRoom(cinemaId, roomId);
            Console.WriteLine("Screening room removed successfully.");
            break;
        } while (true);
    }
}