using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interfaces;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Character;
using MovieAPI.Models.DTO.Franchise;
using MovieAPI.Models.DTO.Movie;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/franchises")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all the Franchises
        /// </summary>
        /// <returns>Returns data of all franchises</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetAllFranchises()
        {
            return _mapper.Map<List<FranchiseReadDTO>>(await _franchiseService.GetAllFranchisesAsync());
        }

        /// <summary>
        /// Fetches a specific Franchise by id.
        /// </summary>
        /// <param name="id">The id of franchise to fetch</param>
        /// <returns>Returns data of given franchise</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            Franchise franchise = await _franchiseService.GetSpecificFranchiseAsync(id);

            if (franchise == null)
                return NotFound();

            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

        /// <summary>
        /// Adds a new franchise to the database.
        /// </summary>
        /// <param name="fdto">The franchise object to add.</param>
        /// <returns>Returns the added franchise data.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO fdto)
        {
            var domainFranchise = _mapper.Map<Franchise>(fdto);

            domainFranchise = await _franchiseService.AddFranchiseAsync(domainFranchise);

            return CreatedAtAction("GetFranchise",
                new { id = domainFranchise.Id },
                _mapper.Map<FranchiseReadDTO>(domainFranchise));
        }

        /// <summary>
        /// Updates a franchise, must pass a full Franchise object and Id in route.
        /// </summary>
        /// <param name="id">The id of franchise to update.</param>
        /// <param name="fdto">The franchise object to update.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFranchise(int id, FranchiseUpdateDTO fdto)
        {
            if (id != fdto.Id)
                return BadRequest();

            if (!_franchiseService.FranchiseExists(id))
                return NotFound();

            Franchise franchise = _mapper.Map<Franchise>(fdto);
            await _franchiseService.UpdateFranchiseAsync(franchise);

            return NoContent();
        }

        /// <summary>
        /// Deletes a franchise from the database by id.
        /// </summary>
        /// <param name="id">The id of franchise to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
                return NotFound();

            await _franchiseService.DeleteFranchiseAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Fetches all the movies in a franchise.
        /// </summary>
        /// <param name="id">The id of franchise to get movies from.</param>
        /// <returns>Returns data of movies.</returns>
        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetFranchiseMovies(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
                return NotFound();

            return _mapper.Map<List<MovieReadDTO>>(await _franchiseService.GetFranchiseMoviesAsync(id));
        }

        /// <summary>
        /// Fetches all the characters in a franchise.
        /// </summary>
        /// <param name="id">The id of franchise to get characters from.</param>
        /// <returns>Returns data of characters.</returns>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetFranchiseCharacters(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
                return NotFound();

            return _mapper.Map<List<CharacterReadDTO>>(await _franchiseService.GetFranchiseCharactersAsync(id));
        }
    }
}
