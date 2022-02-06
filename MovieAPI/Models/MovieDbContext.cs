using Microsoft.EntityFrameworkCore;
using MovieAPI.Models.Domain;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MovieAPI.Models
{
    public class MovieDbContext : DbContext
    {

        //Tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set;}

        public MovieDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 1, 
                Title = "The Avengers", 
                Genre = "Action, Adventure, Sci-Fi", 
                ReleaseYear = "2012", 
                Director = "Joss Whedon", 
                Picture = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg", 
                Trailer = "https://www.youtube.com/watch?v=eOrNdBpGMv8", 
                FranchiseId = 1
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2, 
                Title = "Avengers: Age of Ultron", 
                Genre = "Action, Adventure, Sci-Fi", 
                ReleaseYear = "2015", 
                Director = "Joss Whedon", 
                Picture = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_FMjpg_UX1000_.jpg", 
                Trailer = "https://www.youtube.com/watch?v=tmeOjFno6Do", 
                FranchiseId = 1
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 3, 
                Title = "John Wick", 
                Genre = "Action, Crime, Thriller", 
                ReleaseYear = "2014", 
                Director = "Chad Stahelski", 
                Picture = "https://m.media-amazon.com/images/M/MV5BMTU2NjA1ODgzMF5BMl5BanBnXkFtZTgwMTM2MTI4MjE@._V1_.jpg", 
                Trailer = "https://www.youtube.com/watch?v=C0BMx-qxsP4", 
                FranchiseId = 2
            });

            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 1, 
                Name = "Robert Downey Jr.", 
                Alias = "Tony Stark", 
                Gender = "Male", 
                Picture = "https://img.joomcdn.net/dace9a3da47d7d748e13af43f96344a4449c7688_original.jpeg"
            });
            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 2, 
                Name = "Chris Evans", 
                Alias = "Steve Rodgers", 
                Gender = "Male", 
                Picture = "https://p.favim.com/orig/2018/12/19/captain-america-steve-rodgers-Favim.com-6694404.jpg"
            });
            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 3, 
                Name = "Scarlett Johansson", 
                Alias = "Natasha Romanoff", 
                Gender = "Female", 
                Picture = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg/250px-Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg"
            });
            modelBuilder.Entity<Character>().HasData(new Character
            {
                Id = 4,
                Name = "Keanu Reeves",
                Alias = "John Wick",
                Gender = "Male",
                Picture = "https://assets-prd.ignimgs.com/2020/08/06/john-wick-button-1596757524663.jpg"
            });

            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 1, Name = "Marvel Cinematic Universe",
                Description =
                    "The Marvel Cinematic Universe (MCU) is an American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios. The films are based on characters that appear in American comic books published by Marvel Comics."
            });
            modelBuilder.Entity<Franchise>().HasData(new Franchise
            {
                Id = 2,
                Name = "John Wick",
                Description =
                    "John Wick is an American neo-noir action-thriller media franchise created by screenwriter Derek Kolstad and starring Keanu Reeves as John Wick, a former hitman who is forced back into the criminal underworld he abandoned."
            });

            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Characters)
                .WithMany(m => m.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCharacter",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    je =>
                    {
                        je.HasKey("CharacterId", "MovieId");
                        je.HasData(
                            new { CharacterId = 1, MovieId = 1 },
                            new { CharacterId = 2, MovieId = 1 },
                            new { CharacterId = 3, MovieId = 1 },
                            new { CharacterId = 1, MovieId = 2 },
                            new { CharacterId = 2, MovieId = 2 },
                            new { CharacterId = 3, MovieId = 2 },
                            new { CharacterId = 4, MovieId = 3 }
                        );
                    });
        }
    }
}
