using MovieShelf.Models;
using MovieShelf.Services;

MovieService movieService = new MovieService();

List<Movie> movies = movieService.GetMovies();

foreach (var m in movies)
{
    Console.WriteLine($"{m.Title} ({m.Year})");
}