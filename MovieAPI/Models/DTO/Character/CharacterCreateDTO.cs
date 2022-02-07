using System.Collections.Generic;

namespace MovieAPI.Models.DTO.Character
{
    public class CharacterCreateDTO
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
