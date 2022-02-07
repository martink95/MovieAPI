using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet]
        public async Task<IEnumerable<CharacterReadDTO>> GetAllCharacters()
        {
            return _mapper.Map<List<CharacterReadDTO>>(await _characterService.GetAllCharactersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            Character character = await _characterService.GetSpecificCharacterAsync(id);

            if (character == null)
                return NotFound();

            return _mapper.Map<CharacterReadDTO>(character);
        }

        [HttpPut("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO cdto)
        {
            var domainCharacter = _mapper.Map<Character>(cdto);

            domainCharacter = await _characterService.AddCharacterAsync(domainCharacter);

            return CreatedAtAction("GetCharacter",
                new { id = domainCharacter.Id },
                _mapper.Map<CharacterReadDTO>(domainCharacter));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_characterService.CharacterExists(id))
                return NotFound();

            await _characterService.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}
