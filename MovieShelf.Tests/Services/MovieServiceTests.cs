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

    [Fact]
    public void SearchMovies_WithGenre_ReturnsCaseInsensitiveMatches()
    {
        var movies = _movieService.SearchMovies(genre: "sci-fi");

        Assert.Equal(
            new[] { "Inception", "Interstellar" }.Order(),
            movies.Select(movie => movie.Title).Order());
    }

    [Fact]
    public void SearchMovies_WithWhitespaceGenre_DoesNotApplyGenreFilter()
    {
        var movies = _movieService.SearchMovies(genre: " ", minimumYear: 2014);

        Assert.Equal(
            new[] { "Interstellar", "Parasite", "Grand Budapest Hotel" }.Order(),
            movies.Select(movie => movie.Title).Order());
    }

    [Fact]
    public void SearchMovies_WithMinimumYear_ReturnsMatchingMovies()
    {
        var movies = _movieService.SearchMovies(minimumYear: 2014);

        Assert.Equal(
            new[] { "Interstellar", "Parasite", "Grand Budapest Hotel" }.Order(),
            movies.Select(movie => movie.Title).Order());
    }

    [Fact]
    public void SearchMovies_WithMinimumRating_ReturnsMatchingMovies()
    {
        var movies = _movieService.SearchMovies(minimumRating: 9);

        Assert.Equal(
            new[]
            {
                "Der Pate",
                "Pulp Fiction",
                "Spirited Away - Chihiros Reise ins Zauberland",
                "Interstellar",
                "Parasite",
                "Das Leben der Anderen",
                "Grand Budapest Hotel"
            }.Order(),
            movies.Select(movie => movie.Title).Order());
    }

    [Fact]
    public void SearchMovies_WithMultipleFilters_ReturnsMoviesMatchingAllFilters()
    {
        var movies = _movieService.SearchMovies("Sci-Fi", minimumYear: 2012, minimumRating: 9);

        var movie = Assert.Single(movies);
        Assert.Equal("Interstellar", movie.Title);
    }

    [Fact]
    public void SearchMovies_WithoutFilters_ReturnsAllMovies()
    {
        var movies = _movieService.SearchMovies();
        var expectedMovies = _movieService.GetMovies();

        Assert.Equal(
            expectedMovies
                .OrderBy(movie => movie.Id)
                .Select(movie => new { movie.Id, movie.Title, movie.Year, movie.Genre, movie.Rating }),
            movies
                .OrderBy(movie => movie.Id)
                .Select(movie => new { movie.Id, movie.Title, movie.Year, movie.Genre, movie.Rating }));
    }

    [Fact]
    public void SearchMovies_WithMinimumYearBelowRange_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _movieService.SearchMovies(minimumYear: 0));
    }

    [Fact]
    public void SearchMovies_WithMinimumYearAboveCurrentYear_ThrowsArgumentOutOfRangeException()
    {
        int futureYear = DateTime.UtcNow.Year + 1;

        Assert.Throws<ArgumentOutOfRangeException>(() => _movieService.SearchMovies(minimumYear: futureYear));
    }

    [Fact]
    public void SearchMovies_WithMinimumRatingBelowRange_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _movieService.SearchMovies(minimumRating: -1));
    }

    [Fact]
    public void SearchMovies_WithMinimumRatingAboveRange_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _movieService.SearchMovies(minimumRating: 11));
    }
}
