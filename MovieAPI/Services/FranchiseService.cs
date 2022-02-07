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

        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .ToListAsync();
        }

        public async Task<Franchise> GetSpecificFranchiseAsync(int id)
        {
            return await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f=> f.Id == id);
        }

        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(f => f.Id == id);
        }

    }
}
