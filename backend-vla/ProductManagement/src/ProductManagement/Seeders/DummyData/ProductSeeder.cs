namespace ProductManagement.Seeders.DummyData;

using AutoBogus;
using ProductManagement.Domain.Products;
using ProductManagement.Databases;
using System.Linq;

public static class ProductSeeder
{
    public static void SeedSampleProductData(ProductsDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.Add(new AutoFaker<Product>());
            context.Products.Add(new AutoFaker<Product>());
            context.Products.Add(new AutoFaker<Product>());

            context.SaveChanges();
        }
    }
}