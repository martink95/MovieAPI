using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models.Domain;

namespace MovieAPI.Interfaces
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetAllMoviesAsync();
        public Task<Movie> GetSpecificMovieAsync(int id);
        public Task<Movie> AddMovieAsync(Movie movie);
        public Task UpdateMovieAsync(Movie movie);
        public Task DeleteMovieAsync(int id);
        public Task UpdateMovieCharacters(int movieId, List<int> characters);
        public bool MovieExists(int id);
    }
}
