using MovieShelf.Services;
using MovieShelf.Models;
MovieService movieService = new MovieService();

List<Movie> movies = movieService.GetMovies();

foreach (var movie in movies)
{
    Console.WriteLine($"{movie.Title} ({movie.Year})");
}