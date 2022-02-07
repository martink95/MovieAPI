using System.Collections.Generic;

namespace MovieAPI.Models.DTO.Franchise
{
    public class FranchiseReadDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Movies { get; set; }
    }
}
