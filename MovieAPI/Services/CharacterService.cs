using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Models.Domain;

namespace MovieAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieDbContext _context;

        public CharacterService(MovieDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetch all the Characters from character table.
        /// </summary>
        /// <returns>Returns an IEnumerable of all the characters.</returns>
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Include(c => c.Movies)
                .ToListAsync();
        }

        /// <summary>
        /// Fetches the Character by id from the character table.
        /// </summary>
        /// <param name="id">The id of which character to fetch</param>
        /// <returns>Returns the character.</returns>
        public async Task<Character> GetSpecificCharacterAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }

        /// <summary>
        /// Adds a Character to the table.
        /// </summary>
        /// <param name="character">The haracter that is going to be added.</param>
        /// <returns>returns the added character.</returns>
        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        /// <summary>
        /// Updates the character in character table.
        /// </summary>
        /// <param name="character">The character to modify.</param>
        /// <returns></returns>
        public async Task UpdateCharacterAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the Character from the database.
        /// </summary>
        /// <param name="id">The id of the character to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the character exists.
        /// </summary>
        /// <param name="id">The id to check.</param>
        /// <returns>True or false.</returns>
        public bool CharacterExists(int id)
        {
            return _context.Characters.Any(c => c.Id == id);
        }
    }
}
