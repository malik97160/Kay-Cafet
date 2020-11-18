using Application.Common.Interfaces;
using Common;
using Domain.Common;
using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class KayCafetDbContext :  DbContext, IKayCafetDbContext
    {
        public KayCafetDbContext(DbContextOptions<KayCafetDbContext> options)
            : base(options)
        {
        }

        //private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public DbSet<User> Users { get; set; }

        public KayCafetDbContext(DbContextOptions<KayCafetDbContext> options, IDateTime dateTime, ICurrentUserService currentUserService)
            : base(options)
        {
            //_dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
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
