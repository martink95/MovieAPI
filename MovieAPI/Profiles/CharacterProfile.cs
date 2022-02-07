using System.Linq;
using AutoMapper;
using MovieAPI.Models.Domain;
using MovieAPI.Models.DTO.Character;

namespace MovieAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(cdto => cdto.Movies, opt => opt
                    .MapFrom(c => c.Movies
                        .Select(m => m.Id)
                        .ToList()));

            CreateMap<Character, CharacterCreateDTO>()
                .ReverseMap();
            CreateMap<Character, CharacterUpdateDTO>()
                .ReverseMap();
        }
    }
}
