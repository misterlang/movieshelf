using System.Text.Json;
using MovieShelf.Models;

namespace MovieShelf.Services;

public class MovieService
{
    private const string FilePath = "MovieShelf/Data/movies.json";

    public List<Movie> GetMovies()
    {
        string json = File.ReadAllText(FilePath);

        return JsonSerializer.Deserialize<List<Movie>>(json) ?? [];
    }

    public void AddMovie(Movie movie)
    {
        List<Movie> movies = GetMovies();

        movies.Add(movie);

        string json = JsonSerializer.Serialize(movies);

        File.WriteAllText(FilePath, json);
    }

    public List<Movie> FindMoviesByGenre(string genre)
    {
        List<Movie> movies = GetMovies();

        return movies
            .Where(movie => movie.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Movie? GetMovieByTitle(string title)
    {
        return GetMovies()
            .FirstOrDefault(movie =>
                string.Equals(
                    movie.Title,
                    title,
                    StringComparison.OrdinalIgnoreCase));
    }
}