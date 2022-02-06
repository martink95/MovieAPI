using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        // Primary Key
        public int Id { get; set; }

        // Fields
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        // No max length set because Genres are comma separated and a movie
        // can have multiple genres. like Comedy,Action,Romantic,Family and so on.
        [Required]
        public string Genre { get; set; }
        [Required]
        [MaxLength(4)]
        public string ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }
        // No max length set because URLs can be long strings.
        public string Picture { get; set; }
        // No max length set because URL will probably be link to youtube trailer.
        public string Trailer { get; set; }

        // Relationships
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
