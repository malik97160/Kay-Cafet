using Application.Common.Interfaces;
using Common;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.CodeConfiguration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class KayCafetDbContext : DbContext, IKayCafetDbContext
    {
        public KayCafetDbContext(DbContextOptions<KayCafetDbContext> options)
            : base(options)
        {
        }

        //private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public KayCafetDbContext(DbContextOptions<KayCafetDbContext> options, IDateTime dateTime, ICurrentUserService currentUserService)
            : base(options)
        {
            //_dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var refreshBuilder = modelBuilder.Entity<RefreshToken>();
            new RefreshTokenCodeConfiguration().Configure(refreshBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            refreshBuilder.ToTable("RefreshTokens");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedDate = DateTime.Now; //_dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedDate = DateTime.Now; //_dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
