using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interfaces;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Character;
using MovieAPI.Models.DTO.Movie;

namespace MovieAPI.Controllers
{
    [Route("api/characters")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {

        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the Characters.
        /// </summary>
        /// <returns>Returns data of all characters.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CharacterReadDTO>> GetAllCharacters()
        {
            return _mapper.Map<List<CharacterReadDTO>>(await _characterService.GetAllCharactersAsync());
        }

        /// <summary>
        /// Fetches a Character by Id.
        /// </summary>
        /// <param name="id">The character id to fetch from.</param>
        /// <returns>Return data of given character.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            Character character = await _characterService.GetSpecificCharacterAsync(id);

            if (character == null)
                return NotFound();

            return _mapper.Map<CharacterReadDTO>(character);
        }

        /// <summary>
        /// Updates a character, must pass a full Character object and Id to route.
        /// </summary>
        /// <param name="id">The id of character to update.</param>
        /// <param name="cdto">The character object to update.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCharacter(int id, CharacterUpdateDTO cdto)
        {
            if (id != cdto.Id)
                return BadRequest();

            if (!_characterService.CharacterExists(id))
                return NotFound();

            Character character = _mapper.Map<Character>(cdto);
            await _characterService.UpdateCharacterAsync(character);

            return NoContent();
        }

        /// <summary>
        /// Adds a new Character to database.
        /// </summary>
        /// <param name="cdto">The character object to add.</param>
        /// <returns>Returns character added.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO cdto)
        {
            var domainCharacter = _mapper.Map<Character>(cdto);

            domainCharacter = await _characterService.AddCharacterAsync(domainCharacter);

            return CreatedAtAction("GetCharacter",
                new { id = domainCharacter.Id },
                _mapper.Map<CharacterReadDTO>(domainCharacter));
        }

        /// <summary>
        /// Deletes Character from database by id.
        /// </summary>
        /// <param name="id">The id of character to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_characterService.CharacterExists(id))
                return NotFound();

            await _characterService.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}
