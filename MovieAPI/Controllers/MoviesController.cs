using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Interfaces;
using MovieAPI.Models;
using MovieAPI.Models.Domain;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMovies()
        {
            return _mapper.Map<List<MovieReadDTO>>(await _movieService.GetAllMoviesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            Movie movie = await _movieService.GetSpecificMovieAsync(id);

            if (movie == null)
                return NotFound();

            return _mapper.Map<MovieReadDTO>(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO mdto)
        {
            var domainMovie = _mapper.Map<Movie>(mdto);

            domainMovie = await _movieService.AddMovieAsync(domainMovie);

            return CreatedAtAction("GetMovie",
                new {id = domainMovie.Id},
                _mapper.Map<MovieReadDTO>(domainMovie));
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_movieService.MovieExists(id))
                return NotFound();

            await _movieService.DeleteMovieAsync(id);

            return NoContent();
        }

        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharacters(int id, List<int> characterIds)
        {
            if (!_movieService.MovieExists(id))
                return NotFound();

            await _movieService.UpdateMovieCharacters(id, characterIds);

            return NoContent();
        }
    }
}
