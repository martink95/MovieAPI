using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models.Domain
{
    [Table("Franchise")]
    public class Franchise
    {
        // Primary Key
        public int Id { get; set; }
        //Fields
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        // Relationships
        public ICollection<Movie> Movies { get; set; }
    }
}
