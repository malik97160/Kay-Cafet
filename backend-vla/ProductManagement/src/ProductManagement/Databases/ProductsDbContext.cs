namespace ProductManagement.Databases;


using ProductManagement.Domain.Products;
using ProductManagement.Domain.Ingredients;
using ProductManagement.Domain.Familys;
using ProductManagement.Domain;
using ProductManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

public class ProductsDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ProductsDbContext(
        DbContextOptions<ProductsDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    #region DbSet Region - Do Not Delete

    public DbSet<Product> Products { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Family> Familys { get; set; }
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