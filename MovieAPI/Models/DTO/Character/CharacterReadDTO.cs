﻿using System.Collections.Generic;

namespace MovieAPI.Models.DTO.Character
{
    public class CharacterReadDTO
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public List<int> Movies { get; set; }
    }
}
