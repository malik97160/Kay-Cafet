namespace Ordering.Seeders.DummyData;

using AutoBogus;
using Ordering.Domain.OrderLines;
using Ordering.Databases;
using System.Linq;

public static class OrderLineSeeder
{
    public static void SeedSampleOrderLineData(OrdersDbContext context)
    {
        if (!context.OrderLines.Any())
        {
            context.OrderLines.Add(new AutoFaker<OrderLine>());
            context.OrderLines.Add(new AutoFaker<OrderLine>());
            context.OrderLines.Add(new AutoFaker<OrderLine>());

            context.SaveChanges();
        }
    }
}