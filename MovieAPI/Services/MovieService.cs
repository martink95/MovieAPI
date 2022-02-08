using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Models.Domain;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetch all movies from Movie table.
        /// </summary>
        /// <returns>Returns an IEnumerable of movies.</returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .ToListAsync();
        }

        /// <summary>
        /// Fetch the movie by id from movie table.
        /// </summary>
        /// <param name="id">The id to fetch.</param>
        /// <returns>Returns the movie.</returns>
        public async Task<Movie> GetSpecificMovieAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        /// <summary>
        /// Adds a new movie to the movie table.
        /// </summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>Returns the movie added.</returns>
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        /// <summary>
        /// Update the movie in movie table.
        /// </summary>
        /// <param name="movie">The movie to modify</param>
        /// <returns></returns>
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete the movie from movie table.
        /// </summary>
        /// <param name="id">The id of the movie to delete.</param>
        /// <returns></returns>
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Check if the movie exists.
        /// </summary>
        /// <param name="id">The id to check.</param>
        /// <returns>True or False.</returns>
        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

        /// <summary>
        /// Update characters in a given movie.
        /// </summary>
        /// <param name="movieId">The movie to add characters to.</param>
        /// <param name="characterIds">The character ids to add to movie.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateMovieCharacters(int movieId, List<int> characterIds)
        {
            Movie movieToUpdateCharacters = await _context.Movies
                .Include(m => m.Characters)
                .Where(c => c.Id == movieId)
                .FirstAsync();

            List<Character> characters = new();
            foreach (int charIds in characterIds)
            {
                Character chara = await _context.Characters.FindAsync(charIds);
                if (chara == null)
                    throw new KeyNotFoundException();
                characters.Add(chara);
            }

            movieToUpdateCharacters.Characters = characters;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Fetch all characters in given movie.
        /// </summary>
        /// <param name="id">The movie id to get characters from.</param>
        /// <returns>Returns an IEnumerable of characters.</returns>
        public async Task<IEnumerable<Character>> GetMovieCharactersAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstAsync(m => m.Id == id);

            var characters = new List<Character>();

            foreach(var character in movie.Characters) characters.Add(character);

            return characters;
        }
        
    }
}
