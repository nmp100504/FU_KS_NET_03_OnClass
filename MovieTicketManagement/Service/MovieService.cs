public class MovieService : IMovieService
{
    private List<Movie> movies = new List<Movie>();
    private int nextMovieId = 1;

    public Movie GetMovie(int id)
    {
        return movies.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Movie> GetAllMovies()
    {
        return movies;
    }

    public Movie CreateMovie(Movie movie)
    {
        movie.Id = nextMovieId++;
        movies.Add(movie);
        return movie;
    }

    public Movie UpdateMovie(Movie movie)
    {
        var existingMovie = movies.FirstOrDefault(m => m.Id == movie.Id);
        if (existingMovie != null)
        {
            existingMovie.Title = movie.Title;
            existingMovie.Genre = movie.Genre;
            existingMovie.Duration = movie.Duration;
        }
        return existingMovie;
    }

    public void DeleteMovie(int id)
    {
        movies.RemoveAll(m => m.Id == id);
    }
}