namespace ProductManagement.Seeders.DummyData;

using AutoBogus;
using ProductManagement.Domain.Ingredients;
using ProductManagement.Databases;
using System.Linq;

public static class IngredientSeeder
{
    public static void SeedSampleIngredientData(ProductsDbContext context)
    {
        if (!context.Ingredients.Any())
        {
            context.Ingredients.Add(new AutoFaker<Ingredient>());
            context.Ingredients.Add(new AutoFaker<Ingredient>());
            context.Ingredients.Add(new AutoFaker<Ingredient>());

            context.SaveChanges();
        }
    }
}