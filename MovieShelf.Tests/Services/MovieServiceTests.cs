using MovieShelf.Services;
using Xunit;

namespace MovieShelf.Tests.Services;

public class MovieServiceTests
{
    private readonly MovieService _movieService = new();

    [Fact]
    public void GetMovieByTitle_WithExactTitle_ReturnsMovie()
    {
        var movie = _movieService.GetMovieByTitle("Alien");

        Assert.NotNull(movie);
        Assert.Equal("Alien", movie.Title);
    }

    [Fact]
    public void GetMovieByTitle_IsCaseInsensitive_ReturnsMovie()
    {
        var movie = _movieService.GetMovieByTitle("alien");

        Assert.NotNull(movie);
        Assert.Equal("Alien", movie.Title);
    }

    [Fact]
    public void GetMovieByTitle_WithUnknownTitle_ReturnsNull()
    {
        var movie = _movieService.GetMovieByTitle("Dieser Film Existiert Nicht");

        Assert.Null(movie);
    }
}
