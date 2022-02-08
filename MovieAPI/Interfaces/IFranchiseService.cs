using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models.Domain;

namespace MovieAPI.Interfaces
{
    public interface IFranchiseService
    {
        public Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        public Task<Franchise> GetSpecificFranchiseAsync(int id);
        public Task<Franchise> AddFranchiseAsync(Franchise franchise);
        public Task UpdateFranchiseAsync(Franchise franchise);
        public Task DeleteFranchiseAsync(int id);
        public Task<IEnumerable<Movie>> GetFranchiseMoviesAsync(int franchiseId);
        public Task<IEnumerable<Character>> GetFranchiseCharactersAsync(int franchiseId);
        public bool FranchiseExists(int id);
    }
}
