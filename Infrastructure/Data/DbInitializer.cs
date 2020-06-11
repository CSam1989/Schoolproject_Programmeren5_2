using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Entities;
using Infrastructure.Persistence;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
                return; //Als products niet leeg is, dan moet de db niet geseed worden

            var productsJson =
                File.ReadAllText(
                    "C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog5/Projects/Webshop_PR5_R0785485/Infrastructure/Data/productData.json");

            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            foreach (var product in products) context.Products.Add(product);

            context.SaveChanges();
        }
    }
}