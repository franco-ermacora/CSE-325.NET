using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

        context.Database.Migrate();

        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie
            {
                Title = "Fullmetal Alchemist: The Sacred Star of Milos",
                ReleaseDate = new DateTime(2011, 7, 2),
                Genre = "Animation",
                Price = 9.99m,
                Rating = "PG-13"
            },

            new Movie
            {
                Title = "My Hero Academia: Two Heroes",
                ReleaseDate = new DateTime(2018, 8, 3),
                Genre = "Animation",
                Price = 10.99m,
                Rating = "PG-13"
            },

            new Movie
            {
                Title = "Megalo Box",
                ReleaseDate = new DateTime(2018, 4, 6),
                Genre = "Animation",
                Price = 8.99m,
                Rating = "TV-14"
            },

            new Movie
            {
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                ReleaseDate = new DateTime(2001, 12, 19),
                Genre = "Fantasy",
                Price = 12.99m,
                Rating = "PG-13"
            }
        );

        context.SaveChanges();
    }
}