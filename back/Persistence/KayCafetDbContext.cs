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
    public class KayCafetDbContext :  ApiAuthorizationDbContext<User>, IKayCafetDbContext
    {
        //private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;

        public KayCafetDbContext(/*IDateTime dateTime,*/ ICurrentUserService currentUserService, DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            :base(options, operationalStoreOptions)
        {
            //_dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users"); //.Property(m => m.Id).HasColumnName("UserId");
            //modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(m => m.Id).HasColumnName("UserId");
            modelBuilder.Entity<Role>().ToTable("Roles"); //.Property(m => m.Id).HasColumnName("RoleId");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(m => m.Id).HasColumnName("RoleId");
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
