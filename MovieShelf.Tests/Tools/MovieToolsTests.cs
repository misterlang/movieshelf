using Microsoft.Extensions.Hosting;
using MovieShelf.Models;
using MovieShelf.Services;
using MovieShelf.Tools;
using Xunit;

namespace MovieShelf.Tests.Tools;

public class MovieToolsTests
{
    private readonly MovieService _movieService = CreateMovieService();

    [Fact]
    public void GetAllMovies_ReturnsCompleteMovieCollection()
    {
        var movieTools = new MovieTools(_movieService);
        var expectedMovies = _movieService.GetMovies();

        var movies = movieTools.GetAllMovies();

        var expectedMovieDetails = expectedMovies
            .OrderBy(movie => movie.Id)
            .Select(movie => new { movie.Id, movie.Title, movie.Year, movie.Genre, movie.Rating });
        var movieDetails = movies
            .OrderBy(movie => movie.Id)
            .Select(movie => new { movie.Id, movie.Title, movie.Year, movie.Genre, movie.Rating });

        Assert.Equal(expectedMovieDetails, movieDetails);
    }

    [Fact]
    public void GetAllMovies_ReturnsReadOnlyCollection()
    {
        var movieTools = new MovieTools(_movieService);

        var movies = movieTools.GetAllMovies();
        var mutableMovies = Assert.IsAssignableFrom<IList<Movie>>(movies);

        Assert.Throws<NotSupportedException>(() => mutableMovies.Clear());
    }

    private static MovieService CreateMovieService()
    {
        var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = AppContext.BaseDirectory
        });

        return new MovieService(builder.Environment);
    }
}