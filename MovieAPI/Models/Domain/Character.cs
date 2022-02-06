using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models.Domain
{
    [Table("Character")]
    public class Character
    {
        // Primary Key
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Alias { get; set; }
        [MaxLength(15)]
        public string Gender { get; set; }
        // No max length set to picture because URLs can be long strings.
        public string Picture { get; set; }

        // Relationships
        public ICollection<Movie> Movies { get; set; }
    }
}
