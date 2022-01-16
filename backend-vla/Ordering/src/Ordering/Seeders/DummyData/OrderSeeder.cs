namespace Ordering.Seeders.DummyData;

using AutoBogus;
using Ordering.Domain.Orders;
using Ordering.Databases;
using System.Linq;

public static class OrderSeeder
{
    public static void SeedSampleOrderData(OrdersDbContext context)
    {
        if (!context.Orders.Any())
        {
            context.Orders.Add(new AutoFaker<Order>());
            context.Orders.Add(new AutoFaker<Order>());
            context.Orders.Add(new AutoFaker<Order>());

            context.SaveChanges();
        }
    }
}