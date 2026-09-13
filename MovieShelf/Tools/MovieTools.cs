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
    [Description("Findet Filme aus der Sammlung anhand ihres Genres."), DisplayName("Find Movies By Genre"), Category("Movie Tools"),]
    public List<Movie> FindMoviesByGenre(
        [Description("Das gesuchte Filmgenre.")]
        string genre)
    {
        return _movieService.FindMoviesByGenre(genre);
    }
}