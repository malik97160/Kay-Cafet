namespace Ordering.Databases;


using Ordering.Domain.Orders;
using Ordering.Domain.OrderLines;
using Ordering.Domain;
using Ordering.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

public class OrdersDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public OrdersDbContext(
        DbContextOptions<OrdersDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    #region DbSet Region - Do Not Delete

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    #endregion DbSet Region - Do Not Delete

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
        
    private void UpdateAuditFields()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService?.UserId;
                    entry.Entity.CreatedOn = now;
                    entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                    entry.Entity.LastModifiedOn = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService?.UserId;
                    entry.Entity.LastModifiedOn = now;
                    break;
                
                case EntityState.Deleted:
                    // deleted_at
                    break;
            }
        }
    }
}