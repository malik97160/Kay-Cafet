namespace ProductManagement.Seeders.DummyData;

using AutoBogus;
using ProductManagement.Domain.Familys;
using ProductManagement.Databases;
using System.Linq;

public static class FamilySeeder
{
    public static void SeedSampleFamilyData(ProductsDbContext context)
    {
        if (!context.Familys.Any())
        {
            context.Familys.Add(new AutoFaker<Family>());
            context.Familys.Add(new AutoFaker<Family>());
            context.Familys.Add(new AutoFaker<Family>());

            context.SaveChanges();
        }
    }
}