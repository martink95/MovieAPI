using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Models.Domain;

namespace MovieAPI.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _context;

        public FranchiseService(MovieDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches all the Franchises from the franchise table.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .ToListAsync();
        }

        /// <summary>
        /// Fetches the franchise by id from the franchise table.
        /// </summary>
        /// <param name="id">The franchise id to fetch.</param>
        /// <returns></returns>
        public async Task<Franchise> GetSpecificFranchiseAsync(int id)
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f=> f.Id == id);
        }

        /// <summary>
        /// Adds a new franchise to the franchise table.
        /// </summary>
        /// <param name="franchise">The franchise to be added.</param>
        /// <returns>Returns the franchise added.</returns>
        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        /// <summary>
        /// Updates the franchise in franchise table.
        /// </summary>
        /// <param name="franchise">The franchise to modify.</param>
        /// <returns></returns>
        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the franchise from table.
        /// </summary>
        /// <param name="id">The id of the franchise to delete.</param>
        /// <returns></returns>
        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Check if the franchise exists in table.
        /// </summary>
        /// <param name="id">The id to check.</param>
        /// <returns>True or False.</returns>
        public bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(f => f.Id == id);
        }

        /// <summary>
        /// Fetch all movies in given franchise.
        /// </summary>
        /// <param name="id">The id of the franchise to fetch movies from.</param>
        /// <returns>Returns an IEnumerable of all movies in franchise.</returns>
        public async Task<IEnumerable<Movie>> GetFranchiseMoviesAsync(int id)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstAsync(f => f.Id == id);

            var movies = new List<Movie>();

            foreach (var movie in franchise.Movies)
                movies.Add(movie);
            return movies;
        }

        /// <summary>
        /// Fetch all characters in given franchise.
        /// </summary>
        /// <param name="id">The id of the franchise to fetch characters from.</param>
        /// <returns>Returns an IEnumerable of all the characters in franchise.</returns>
        public async Task<IEnumerable<Character>> GetFranchiseCharactersAsync(int id)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .ThenInclude(m => m.Characters)
                .FirstAsync(f => f.Id == id);

            var characters = new List<Character>();
            foreach (var movie in franchise.Movies)
            {
                foreach (var character in movie.Characters)
                {
                    characters.Add(character);
                }
            }
            return characters;
        }
    }
}
