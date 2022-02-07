using System.Linq;
using AutoMapper;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Franchise;

namespace MovieAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(fdto => fdto.Movies, opt => opt
                    .MapFrom(f => f.Movies
                        .Select(m => m.Id)
                        .ToList()));

            CreateMap<Franchise, FranchiseCreateDTO>()
                .ReverseMap();
            CreateMap<Franchise, FranchiseUpdateDTO>()
                .ReverseMap();
        }
    }
}
