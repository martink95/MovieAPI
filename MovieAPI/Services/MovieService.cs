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

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Characters)
                .ToListAsync();
        }

        public async Task<Movie> GetSpecificMovieAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }

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
    }
}
