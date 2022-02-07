using System.Linq;
using AutoMapper;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Movie;

namespace MovieAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile ()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(mdto => mdto.Characters, opt => opt
                    .MapFrom(m => m.Characters
                        .Select(c => c.Id)))
                .ReverseMap();

            CreateMap<Movie, MovieCreateDTO>()
                .ReverseMap();
            CreateMap<Movie, MovieUpdateDTO>()
                .ReverseMap();
        }
    }
}
