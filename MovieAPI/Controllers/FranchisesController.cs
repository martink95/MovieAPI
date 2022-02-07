using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interfaces;
using MovieAPI.Models.Domain;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetAllFranchises()
        {
            return _mapper.Map<List<FranchiseReadDTO>>(await _franchiseService.GetAllFranchisesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            Franchise franchise = await _franchiseService.GetSpecificFranchiseAsync(id);

            if (franchise == null)
                return NotFound();

            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO fdto)
        {
            var domainFranchise = _mapper.Map<Franchise>(fdto);

            domainFranchise = await _franchiseService.AddFranchiseAsync(domainFranchise);

            return CreatedAtAction("GetFranchise",
                new { id = domainFranchise.Id },
                _mapper.Map<FranchiseReadDTO>(domainFranchise));
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
                return NotFound();

            await _franchiseService.DeleteFranchiseAsync(id);

            return NoContent();
        }

    }
}
