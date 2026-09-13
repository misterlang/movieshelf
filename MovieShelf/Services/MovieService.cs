using System.Text.Json;
using MovieShelf.Models;

namespace MovieShelf.Services;

public class MovieService
{
    private const string FilePath = "Data/movies.json";

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
}