using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Character;
using MovieAPI.Models.DTO.Movie;

namespace MovieAPI.Controllers
{
    
    [Route("api/movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all movies.
        /// </summary>
        /// <returns>Returns movie data.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMovies()
        {
            return _mapper.Map<List<MovieReadDTO>>(await _movieService.GetAllMoviesAsync());
        }

        /// <summary>
        /// Fetches movie by id.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <returns>Returns data of given movie.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            Movie movie = await _movieService.GetSpecificMovieAsync(id);

            if (movie == null)
                return NotFound();

            return _mapper.Map<MovieReadDTO>(movie);
        }

        /// <summary>
        /// Adds a new Movie to database.
        /// </summary>
        /// <param name="mdto">The movie to add to table.</param>
        /// <returns>Returns movie created.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO mdto)
        {
            var domainMovie = _mapper.Map<Movie>(mdto);

            domainMovie = await _movieService.AddMovieAsync(domainMovie);

            return CreatedAtAction("GetMovie",
                new {id = domainMovie.Id},
                _mapper.Map<MovieReadDTO>(domainMovie));
        }

        /// <summary>
        /// Updates Movie, must pass a full Movie object and id to route.
        /// </summary>
        /// <param name="id">The id of movie to update.</param>
        /// <param name="mdto">The movie to update table with.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDTO mdto)
        {
            if (id != mdto.Id)
                return BadRequest();
            

            if (!_movieService.MovieExists(id))
                return NotFound();

            Movie movie = _mapper.Map<Movie>(mdto);
            await _movieService.UpdateMovieAsync(movie);

            return NoContent();
        }

        /// <summary>
        /// Deletes a Movie from database by id.
        /// </summary>
        /// <param name="id">The id of movie to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_movieService.MovieExists(id))
                return NotFound();

            await _movieService.DeleteMovieAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Updates Movie Characters, must pass in id for which movie to update, and list of character ids to add.
        /// </summary>
        /// <param name="id">The id of movie to add to.</param>
        /// <param name="characterIds">List of character ids</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovieCharacters(int id, List<int> characterIds)
        {
            if (!_movieService.MovieExists(id))
                return NotFound();

            await _movieService.UpdateMovieCharacters(id, characterIds);

            return NoContent();
        }

        /// <summary>
        /// Fetches all the characters in a movie
        /// </summary>
        /// <param name="id">The movie id to fetch characters from.</param>
        /// <returns>Returns data of characters in movie.</returns>
        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetAllMovieCharacters(int id)
        {
            if (!_movieService.MovieExists(id))
                return NotFound();

            return _mapper.Map<List<CharacterReadDTO>>(await _movieService.GetMovieCharactersAsync(id));
        }
    }
}
