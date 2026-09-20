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

    public List<Movie> SearchMovies(
        string? genre = null,
        int? minimumYear = null,
        int? minimumRating = null)
    {
        int currentYear = DateTime.UtcNow.Year;

        if (minimumYear.HasValue && (minimumYear.Value < 1 || minimumYear.Value > currentYear))
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumYear),
                $"Minimum year must be between 1 and {currentYear}.");
        }

        if (minimumRating.HasValue && (minimumRating.Value < 0 || minimumRating.Value > 10))
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumRating),
                "Minimum rating must be between 0 and 10.");
        }

        IEnumerable<Movie> movies = GetMovies();

        if (!string.IsNullOrWhiteSpace(genre))
        {
            movies = movies.Where(movie =>
                movie.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
        }

        if (minimumYear.HasValue)
        {
            movies = movies.Where(movie => movie.Year >= minimumYear.Value);
        }

        if (minimumRating.HasValue)
        {
            movies = movies.Where(movie => movie.Rating >= minimumRating.Value);
        }

        return movies.ToList();
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