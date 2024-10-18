public class MovieController
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public void CreateMovie()
    {
        Console.Write("Enter movie title: ");
        string title = Console.ReadLine();
        Console.Write("Enter movie genre: ");
        string genre = Console.ReadLine();
        Console.Write("Enter movie duration (in minutes): ");
        int duration = int.Parse(Console.ReadLine());

        var movie = new Movie { Title = title, Genre = genre, Duration = duration };
        _movieService.CreateMovie(movie);
        Console.WriteLine("Movie created successfully.");
    }

    public void UpdateMovie()
    {
        ListMovies();
        int id;
        do
        {
            Console.Write("Enter movie ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            var movie = _movieService.GetMovie(id);
            if (movie == null)
            {
                Console.WriteLine("Movie not found. Please try again.");
                continue;
            }
            Console.Write("Enter new movie title: ");
            string title = Console.ReadLine();
            Console.Write("Enter new movie genre: ");
            string genre = Console.ReadLine();
            int duration;
            do
            {
                Console.Write("Enter new movie duration (in minutes): ");
                if (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive number.");
                    continue;
                }
                break;
            } while (true);

            movie.Title = title;
            movie.Genre = genre;
            movie.Duration = duration;
            _movieService.UpdateMovie(movie);
            Console.WriteLine("Movie updated successfully.");
            break;
        } while (true);
    }

    public void DeleteMovie()
    {
        ListMovies();
        int id;
        do
        {
            Console.Write("Enter movie ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            var movie = _movieService.GetMovie(id);
            if (movie == null)
            {
                Console.WriteLine("Movie not found. Please try again.");
                continue;
            }
            _movieService.DeleteMovie(id);
            Console.WriteLine("Movie deleted successfully.");
            break;
        } while (true);
    }


    public void ListMovies()
    {
        var movies = _movieService.GetAllMovies();
        foreach (var movie in movies)
        {
            Console.WriteLine($"ID: {movie.Id}, Title: {movie.Title}, Genre: {movie.Genre}, Duration: {movie.Duration} minutes");
        }
    }
}