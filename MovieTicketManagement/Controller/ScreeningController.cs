public class ScreeningController
{
    private readonly IScreeningService _screeningService;
    private readonly IMovieService _movieService;
    private readonly ICinemaService _cinemaService;

    public ScreeningController(IScreeningService screeningService, IMovieService movieService, ICinemaService cinemaService)
    {
        _screeningService = screeningService;
        _movieService = movieService;
        _cinemaService = cinemaService;
    }


    public void CreateScreening()
    {
        Console.Write("Enter movie ID: ");
        int movieId = int.Parse(Console.ReadLine());
        Console.Write("Enter cinema ID: ");
        int cinemaId = int.Parse(Console.ReadLine());
        Console.Write("Enter screening room ID: ");
        int roomId = int.Parse(Console.ReadLine());
        Console.Write("Enter show date and time (yyyy-MM-dd HH:mm): ");
        DateTime showDateTime = DateTime.Parse(Console.ReadLine());

        var movie = _movieService.GetMovie(movieId);
        var cinema = _cinemaService.GetCinema(cinemaId);
        var room = cinema?.ScreeningRooms.FirstOrDefault(r => r.Id == roomId);

        if (movie != null && room != null)
        {
            var screening = new Screening
            {
                MovieId = movieId,
                ScreeningRoomId = roomId,
                ShowDateTime = showDateTime
            };
            _screeningService.CreateScreening(screening);
            Console.WriteLine("Screening created successfully.");
        }
        else
        {
            Console.WriteLine("Failed to create screening. Movie or screening room not found.");
        }
    }

    public void UpdateScreening()
    {
        Console.Write("Enter screening ID to update: ");
        int id = int.Parse(Console.ReadLine());
        var screening = _screeningService.GetScreening(id);
        if (screening != null)
        {
            Console.Write("Enter new movie ID: ");
            int movieId = int.Parse(Console.ReadLine());
            Console.Write("Enter new cinema ID: ");
            int cinemaId = int.Parse(Console.ReadLine());
            Console.Write("Enter new screening room ID: ");
            int roomId = int.Parse(Console.ReadLine());
            Console.Write("Enter new show date and time (yyyy-MM-dd HH:mm): ");
            DateTime showDateTime = DateTime.Parse(Console.ReadLine());

            var movie = _movieService.GetMovie(movieId);
            var cinema = _cinemaService.GetCinema(cinemaId);
            var room = cinema?.ScreeningRooms.FirstOrDefault(r => r.Id == roomId);

            if (movie != null && room != null)
            {
                screening.MovieId = movieId;
                screening.ScreeningRoomId = roomId;
                screening.ShowDateTime = showDateTime;
                _screeningService.UpdateScreening(screening);
                Console.WriteLine("Screening updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update screening. Movie or screening room not found.");
            }
        }
        else
        {
            Console.WriteLine("Screening not found.");
        }
    }

    public void DeleteScreening()
    {
        Console.Write("Enter screening ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        _screeningService.DeleteScreening(id);
        Console.WriteLine("Screening deleted successfully.");
    }
    public void ListScreenings()
    {
        try
        {
            var screenings = _screeningService.GetAllScreenings();
            if (!screenings.Any())
            {
                Console.WriteLine("No screenings available.");
                return;
            }

            foreach (var screening in screenings)
            {
                var movie = _movieService.GetMovie(screening.MovieId);
                var cinema = _cinemaService.GetCinema(screening.ScreeningRoom?.CinemaId ?? 0);
                
                Console.WriteLine($"ID: {screening.Id}");
                Console.WriteLine($"Movie: {movie?.Title ?? "Unknown"}");
                Console.WriteLine($"Cinema: {cinema?.Name ?? "Unknown"}");
                Console.WriteLine($"Room: {screening.ScreeningRoom?.Name ?? "Unknown"}");
                Console.WriteLine($"Show Time: {screening.ShowDateTime}");
                Console.WriteLine("--------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while listing screenings: {ex.Message}");
        }
    }

    public void OrderTicket()
{
        Console.WriteLine("Available Cinemas:");
        var cinemas = _cinemaService.GetAllCinemas().ToList();
        for (int i = 0; i < cinemas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cinemas[i].Name}");
        }
        Console.Write("Select a cinema (enter number): ");
        int cinemaChoice = int.Parse(Console.ReadLine()) - 1;
        var selectedCinema = cinemas[cinemaChoice];

        Console.Write("Enter date for screening (yyyy-MM-dd): ");
        DateTime selectedDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Available Movies:");
        var movies = _movieService.GetAllMovies().ToList();
        for (int i = 0; i < movies.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {movies[i].Title}");
        }
        Console.Write("Select a movie (enter number): ");
        int movieChoice = int.Parse(Console.ReadLine()) - 1;
        var selectedMovie = movies[movieChoice];

        var screenings = _screeningService.GetScreeningsByCinema(selectedCinema.Id)
            .Where(s => s.ShowDateTime.Date == selectedDate.Date && s.MovieId == selectedMovie.Id)
            .ToList();

        if (screenings.Count == 0)
        // {
        //     Console.WriteLine("No screenings available for the selected criteria.");
        //     return;
        // }

        Console.WriteLine("Available Screenings:");
        for (int i = 0; i < screenings.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {screenings[i].ShowDateTime.ToShortTimeString()} - Room: {screenings[i].ScreeningRoom.Name}");
        }
        Console.Write("Select a screening (enter number): ");
        int screeningChoice = int.Parse(Console.ReadLine()) - 1;
        var selectedScreening = screenings[screeningChoice];

        Console.Write("Enter number of tickets: ");
        int numberOfTickets = int.Parse(Console.ReadLine());

        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();
        Console.Write("Enter your phone number: ");
        string userPhone = Console.ReadLine();

        Random random = new Random();
        for (int i = 0; i < numberOfTickets; i++)
        {
            int seatNumber = random.Next(1, 101);
            var ticket = new Ticket
            {
                ScreeningId = selectedScreening.Id,
                CustomerName = userName,
                PhoneNumber = userPhone,
                SeatNumber = seatNumber
            };
            _screeningService.CreateTicket(ticket);
            Console.WriteLine($"Ticket created - Seat: {seatNumber}");
        }

        Console.WriteLine("Tickets ordered successfully!");
    }

    public void ListTickets()
    {
        Console.Write("Enter your phone number: ");
        string phoneNumber = Console.ReadLine();

        var tickets = _screeningService.GetTicketsByPhoneNumber(phoneNumber);
        if (tickets.Count() == 0)
        {
            Console.WriteLine("No tickets found for this phone number.");
            return;
        }

        Console.WriteLine("Your tickets:");
        foreach (var ticket in tickets)
        {
            var screening = _screeningService.GetScreening(ticket.ScreeningId);
            var movie = _movieService.GetMovie(screening.MovieId);
            Console.WriteLine($"ID: {ticket.Id}, Customer Name: {ticket.CustomerName}, Phone: {ticket.PhoneNumber}, Movie: {movie.Title}, Date: {screening.ShowDateTime}, Seat: {ticket.SeatNumber}");
        }
    }
    public void CancelTicket()
    {
        Console.Write("Enter your phone number: ");
        string phoneNumber = Console.ReadLine();

        var tickets = _screeningService.GetTicketsByPhoneNumber(phoneNumber);
        if (tickets.Count() == 0)
        {
            Console.WriteLine("No tickets found for this phone number.");
            return;
        }

        Console.WriteLine("Your tickets:");
        foreach (var ticket in tickets)
        {
            var screening = _screeningService.GetScreening(ticket.ScreeningId);
            var movie = _movieService.GetMovie(screening.MovieId);
            Console.WriteLine($"ID: {ticket.Id}, Customer Name: {ticket.CustomerName}, Phone: {ticket.PhoneNumber}, Movie: {movie.Title}, Date: {screening.ShowDateTime}, Seat: {ticket.SeatNumber}");
        }

        Console.Write("Enter the ID of the ticket you want to cancel: ");
        int ticketId = int.Parse(Console.ReadLine());

        _screeningService.CancelTicket(ticketId);
        Console.WriteLine("Ticket cancelled successfully.");
    }

}