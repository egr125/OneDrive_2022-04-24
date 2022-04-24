using MediaUI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MediaUI.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Post.Any())
                {
                    return;   // DB has been seeded
                }

                context.Post.AddRange(
                    new Post
                    {
                        Title = "New Batman",
                        CreatedDate = DateTime.Parse("2020-2-12"),
                        Category = Post.Categories.Entertainment,
                        Report = "Wow, this movie!",
                        CreatedBy = Guid.NewGuid()
                    },

                    new Post
                    {
                        Title = "Rugby",
                        CreatedDate = DateTime.Parse("2021-2-12"),
                        Category = Post.Categories.Sports,
                        Report = "What a match!",
                        CreatedBy = Guid.NewGuid()
                    },

                    new Post
                    {
                        Title = "War!",
                        CreatedDate = DateTime.Parse("2022-1-1"),
                        Category = Post.Categories.News,
                        Report = "Ukraine",
                        CreatedBy = Guid.NewGuid()
                    }
                );
                //context.Comment.AddRange(
                //    new Comment
                //    {
                //        Descritption = "I just loved that movie",
                //        Report = 1,
                //        CreatedBy = Guid.NewGuid(),
                //        CreatedDate = DateTime.Parse("2022-1-1")
                //    },
                //      new Comment
                //      {
                //          Descritption = "Nah, it's ok",
                //          Report = 1,
                //          CreatedBy = Guid.NewGuid(),
                //          CreatedDate = DateTime.Parse("2022-1-1")
                //      }, new Comment
                //      {
                //          Descritption = "We Lost",
                //          Report = 2,
                //          CreatedBy = Guid.NewGuid(),
                //          CreatedDate = DateTime.Parse("2021-1-1")
                //      }, new Comment
                //      {
                //          Descritption = "So Sad!",
                //          Report = 3,
                //          CreatedBy = Guid.NewGuid(),
                //          CreatedDate = DateTime.Parse("2021-1-1")
                //      });
                context.SaveChanges();
            }
        }
    }
}