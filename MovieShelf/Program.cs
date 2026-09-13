using MovieShelf.Models;
using MovieShelf.Services;

MovieService movieService = new MovieService();

List<Movie> horrorMovies = movieService.FindMoviesByGenre("Horror");

foreach (Movie movie in horrorMovies)
{
    Console.WriteLine($"{movie.Title} ({movie.Year})");
}