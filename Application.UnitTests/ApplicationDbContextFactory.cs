using System;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            context.Products.AddRange(
                new Product
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 1,
                    Category = Category.Fruit,
                    Description = "Jonagold"
                }, new Product
                {
                    Id = 2,
                    Name = "Bread",
                    Price = 2,
                    Category = Category.Bread,
                    Description = "Wholeweat"
                }, new Product
                {
                    Id = 3,
                    Name = "Curry",
                    Price = 3,
                    Category = Category.Seasonings,
                    Description = "Yellow"
                }
            );

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}