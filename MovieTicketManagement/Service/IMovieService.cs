public interface IMovieService
{
    Movie GetMovie(int id);
    IEnumerable<Movie> GetAllMovies();
    Movie CreateMovie(Movie movie);
    Movie UpdateMovie(Movie movie);
    void DeleteMovie(int id);
}