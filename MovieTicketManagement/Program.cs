using System;
using System.Collections.Generic;

class Program
{
    private static CinemaController cinemaController = null!;
    private static MovieController movieController = null!;
    private static ScreeningController screeningController = null!;

    static void Main(string[] args)
    {
        InitializeControllers();

        while (true)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CinemaManagementMenu();
                    break;
                case "2":
                    MovieManagementMenu();
                    break;
                case "3":
                    ScreeningManagementMenu();
                    break;
                case "4":
                    TicketManagementMenu();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void InitializeControllers()
    {
        var cinemaService = new CinemaService();
        var movieService = new MovieService();
        var screeningService = new ScreeningService();

        cinemaController = new CinemaController(cinemaService);
        movieController = new MovieController(movieService);
        screeningController = new ScreeningController(screeningService, movieService, cinemaService);

        // Generate test data
        GenerateTestData(cinemaService, movieService, screeningService);
    }

    static void GenerateTestData(CinemaService cinemaService, MovieService movieService, ScreeningService screeningService)
    {
        // Create test cinemas
        var cinemas = new List<Cinema>
        {
            new Cinema { Id = 1, Name = "Cinema A" },
            new Cinema { Id = 2, Name = "Cinema B" },
            new Cinema { Id = 3, Name = "Cinema C" }
        };

        foreach (var cinema in cinemas)
        {
            cinemaService.CreateCinema(cinema);
        }

        // Create test movies
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie A", Genre = "Action", Duration = 120 },
            new Movie { Id = 2, Title = "Movie B", Genre = "Comedy", Duration = 90 },
            new Movie { Id = 3, Title = "Movie C", Genre = "Drama", Duration = 150 }
        };

        foreach (var movie in movies)
        {
            movieService.CreateMovie(movie);
        }

        // Create test screening rooms
        var screeningRooms = new List<ScreeningRoom>
        {
            new ScreeningRoom { Id = 1, Name = "Screening Room A", CinemaId = 1 },
            new ScreeningRoom { Id = 2, Name = "Screening Room B", CinemaId = 2 },
            new ScreeningRoom { Id = 3, Name = "Screening Room C", CinemaId = 3 }
        };

        foreach (var room in screeningRooms)
        {
            cinemaService.AddScreeningRoom(room.CinemaId, room);
        }

        // Create test screenings
        var screenings = new List<Screening>
        {
            new Screening { Id = 1, MovieId = 1, Movie = movies[0], ScreeningRoomId = 1, ScreeningRoom = screeningRooms[0], ShowDateTime = DateTime.Now.AddHours(2) },
            new Screening { Id = 2, MovieId = 2, Movie = movies[1], ScreeningRoomId = 2, ScreeningRoom = screeningRooms[1], ShowDateTime = DateTime.Now.AddHours(3) },
            new Screening { Id = 3, MovieId = 3, Movie = movies[2], ScreeningRoomId = 3, ScreeningRoom = screeningRooms[2], ShowDateTime = DateTime.Now.AddHours(4) }
        };

        foreach (var screening in screenings)
        {
            screeningService.CreateScreening(screening);
        }
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Movie Ticket Booking System");
        Console.WriteLine("1. Cinema Management");
        Console.WriteLine("2. Movie Management");
        Console.WriteLine("3. Screening Management");
        Console.WriteLine("4. Ticket Management");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice: ");
    }

    static void CinemaManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Cinema Management");
            Console.WriteLine("1. Create Cinema");
            Console.WriteLine("2. Update Cinema");
            Console.WriteLine("3. Delete Cinema");
            Console.WriteLine("4. List Cinemas");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    cinemaController.CreateCinema();
                    break;
                case "2":
                    cinemaController.UpdateCinema();
                    break;
                case "3":
                    cinemaController.DeleteCinema();
                    break;
                case "4":
                    cinemaController.ListCinemas();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void MovieManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Movie Management");
            Console.WriteLine("1. Create Movie");
            Console.WriteLine("2. Update Movie");
            Console.WriteLine("3. Delete Movie");
            Console.WriteLine("4. List Movies");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    movieController.CreateMovie();
                    break;
                case "2":
                    movieController.UpdateMovie();
                    break;
                case "3":
                    movieController.DeleteMovie();
                    break;
                case "4":
                    movieController.ListMovies();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void ScreeningManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Screening Management");
            Console.WriteLine("1. Create Screening");
            Console.WriteLine("2. Update Screening");
            Console.WriteLine("3. Delete Screening");
            Console.WriteLine("4. List Screenings");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    screeningController.CreateScreening();
                    break;
                case "2":
                    screeningController.UpdateScreening();
                    break;
                case "3":
                    screeningController.DeleteScreening();
                    break;
                case "4":
                    screeningController.ListScreenings();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void TicketManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Ticket Management");
            Console.WriteLine("1. Order Ticket");
            Console.WriteLine("2. List Tickets");
            Console.WriteLine("3. Cancel Ticket");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    screeningController.OrderTicket();
                    break;
                case "2":
                    screeningController.ListTickets();
                    break;
                case "3":
                    screeningController.CancelTicket();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
