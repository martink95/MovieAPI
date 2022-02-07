using System.Collections.Generic;
using System.Threading.Tasks;
using MovieAPI.Models.Domain;

namespace MovieAPI.Interfaces
{
    public interface ICharacterService
    {
        public Task<IEnumerable<Character>> GetAllCharactersAsync();
        public Task<Character> GetSpecificCharacterAsync(int id);
        public Task<Character> AddCharacterAsync(Character character);
        public Task UpdateCharacterAsync(Character character);
        public Task DeleteCharacterAsync(int id);

        public bool CharacterExists(int id);
    }
}
