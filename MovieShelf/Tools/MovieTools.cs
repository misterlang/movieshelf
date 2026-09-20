using System.ComponentModel;
using ModelContextProtocol.Server;
using MovieShelf.Models;
using MovieShelf.Services;

namespace MovieShelf.Tools;

[McpServerToolType]
public class MovieTools
{
    private readonly MovieService _movieService;

    public MovieTools(MovieService movieService)
    {
        _movieService = movieService;
    }

    [McpServerTool]
    [Description("Gets the complete movie collection.")]
    public IReadOnlyList<Movie> GetAllMovies()
    {
        Console.Error.WriteLine("[MovieShelf] GetAllMovies gestartet.");

        try
        {
            var movies = _movieService.GetMovies().AsReadOnly();

            Console.Error.WriteLine(
                $"[MovieShelf] GetAllMovies erfolgreich. Anzahl Ergebnisse: {movies.Count}");

            return movies;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"[MovieShelf] FEHLER: {ex}");

            throw;
        }
    }

    [McpServerTool]
    [Description("Findet Filme aus der Sammlung anhand ihres Genres."), DisplayName("Find Movies By Genre"), Category("Movie Tools")]
    public IReadOnlyList<Movie> FindMoviesByGenre(
    [Description("Das gesuchte Filmgenre.")]
    string genre)
    {
        Console.Error.WriteLine(
            $"[MovieShelf] FindMoviesByGenre gestartet. Genre: '{genre}'");

        try
        {
            var movies = _movieService.FindMoviesByGenre(genre);

            Console.Error.WriteLine(
                $"[MovieShelf] FindMoviesByGenre erfolgreich. Anzahl Ergebnisse: {movies.Count}");

            return movies.AsReadOnly();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"[MovieShelf] FEHLER: {ex}");

            throw;
        }
    }

    [McpServerTool]
    [Description("Searches movies by optional genre, minimum release year, and minimum rating.")]
    public IReadOnlyList<Movie> SearchMovies(
    [Description("Optional movie genre.")]
    string? genre = null,
    [Description("Optional minimum release year.")]
    int? minimumYear = null,
    [Description("Optional minimum movie rating.")]
    int? minimumRating = null)
    {
        Console.Error.WriteLine(
            $"[MovieShelf] SearchMovies gestartet. Genre: '{genre ?? "<null>"}', Mindestjahr: {minimumYear}, Mindestbewertung: {minimumRating}");

        try
        {
            var movies = _movieService.SearchMovies(genre, minimumYear, minimumRating);

            Console.Error.WriteLine(
                $"[MovieShelf] SearchMovies erfolgreich. Anzahl Ergebnisse: {movies.Count}");

            return movies.AsReadOnly();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"[MovieShelf] FEHLER: {ex}");

            throw;
        }
    }

    [McpServerTool]
    [Description("Finds a movie in the collection by its title.")]
    public Movie? GetMovieByTitle(
    [Description("The exact title of the movie to search for.")]
    string title)
    {
        return _movieService.GetMovieByTitle(title);
    }
}