// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieAPI.Models;

namespace MovieAPI.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieAPI.Models.Domain.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Tony Stark",
                            Gender = "Male",
                            Name = "Robert Downey Jr.",
                            Picture = "https://img.joomcdn.net/dace9a3da47d7d748e13af43f96344a4449c7688_original.jpeg"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "Steve Rodgers",
                            Gender = "Male",
                            Name = "Chris Evans",
                            Picture = "https://p.favim.com/orig/2018/12/19/captain-america-steve-rodgers-Favim.com-6694404.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Natasha Romanoff",
                            Gender = "Female",
                            Name = "Scarlett Johansson",
                            Picture = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg/250px-Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Alias = "John Wick",
                            Gender = "Male",
                            Name = "Keanu Reeves",
                            Picture = "https://assets-prd.ignimgs.com/2020/08/06/john-wick-button-1596757524663.jpg"
                        });
                });

            modelBuilder.Entity("MovieAPI.Models.Domain.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Franchise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The Marvel Cinematic Universe (MCU) is an American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios. The films are based on characters that appear in American comic books published by Marvel Comics.",
                            Name = "Marvel Cinematic Universe"
                        },
                        new
                        {
                            Id = 2,
                            Description = "John Wick is an American neo-noir action-thriller media franchise created by screenwriter Derek Kolstad and starring Keanu Reeves as John Wick, a former hitman who is forced back into the criminal underworld he abandoned.",
                            Name = "John Wick"
                        });
                });

            modelBuilder.Entity("MovieAPI.Models.Domain.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Director")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Trailer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Joss Whedon",
                            FranchiseId = 1,
                            Genre = "Action, Adventure, Sci-Fi",
                            Picture = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg",
                            ReleaseYear = "2012",
                            Title = "The Avengers",
                            Trailer = "https://www.youtube.com/watch?v=eOrNdBpGMv8"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Joss Whedon",
                            FranchiseId = 1,
                            Genre = "Action, Adventure, Sci-Fi",
                            Picture = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_FMjpg_UX1000_.jpg",
                            ReleaseYear = "2015",
                            Title = "Avengers: Age of Ultron",
                            Trailer = "https://www.youtube.com/watch?v=tmeOjFno6Do"
                        },
                        new
                        {
                            Id = 3,
                            Director = "Chad Stahelski",
                            FranchiseId = 2,
                            Genre = "Action, Crime, Thriller",
                            Picture = "https://m.media-amazon.com/images/M/MV5BMTU2NjA1ODgzMF5BMl5BanBnXkFtZTgwMTM2MTI4MjE@._V1_.jpg",
                            ReleaseYear = "2014",
                            Title = "John Wick",
                            Trailer = "https://www.youtube.com/watch?v=C0BMx-qxsP4"
                        });
                });

            modelBuilder.Entity("MovieCharacter", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCharacter");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            MovieId = 1
                        },
                        new
                        {
                            CharacterId = 2,
                            MovieId = 1
                        },
                        new
                        {
                            CharacterId = 3,
                            MovieId = 1
                        },
                        new
                        {
                            CharacterId = 1,
                            MovieId = 2
                        },
                        new
                        {
                            CharacterId = 2,
                            MovieId = 2
                        },
                        new
                        {
                            CharacterId = 3,
                            MovieId = 2
                        },
                        new
                        {
                            CharacterId = 4,
                            MovieId = 3
                        });
                });

            modelBuilder.Entity("MovieAPI.Models.Domain.Movie", b =>
                {
                    b.HasOne("MovieAPI.Models.Domain.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("MovieCharacter", b =>
                {
                    b.HasOne("MovieAPI.Models.Domain.Character", null)
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieAPI.Models.Domain.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieAPI.Models.Domain.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
