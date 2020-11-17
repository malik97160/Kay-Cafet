using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Command
{
    public class SampleDataSeeder
    {
        private readonly IKayCafetDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _adminRole = "Administrator";
        private readonly UserManager<IdentityUser> _userManager;

        /*TODO create a usermanagement service to handle user creation and roles*/

        public SampleDataSeeder(IKayCafetDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task SeedAllDataAsync(CancellationToken cancellationToken)
        {
            await SeedRolesAsync(cancellationToken);
            await SeedUsersAsync(cancellationToken);
        }

        private async Task SeedRolesAsync(CancellationToken cancellationToken)
        {
            if (!(await _roleManager.RoleExistsAsync(_adminRole)))
            {
                var adminRole = new IdentityRole(_adminRole);
                await _roleManager.CreateAsync(adminRole);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            var adminUserEmail = "malik.couchy@gmail.com";
            var adminId = Guid.NewGuid().ToString();
            var adminUser = new User()
            {
                Id = adminId,
                FirstName = "Malik",
                LastName = "Couchy"
            };

            if (await _userManager.FindByEmailAsync(adminUserEmail) == null)
            {
                _context.Users.Add(adminUser);
                var adminIdentity = new IdentityUser() { Email = adminUserEmail, Id = adminId, EmailConfirmed = true, LockoutEnabled = false, PhoneNumber = "0678956820" };
                await _userManager.CreateAsync(adminIdentity);
                await _userManager.AddToRoleAsync(adminIdentity, _adminRole);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
