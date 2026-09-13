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
}