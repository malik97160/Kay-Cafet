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
        private readonly RoleManager<Role> _roleManager;
        private readonly string _adminRole = "Administrator";
        private readonly UserManager<User> _userManager;

        /*TODO create a usermanagement service to handle user creation and roles*/

        public SampleDataSeeder(IKayCafetDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
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
                var adminRole = new Role() { 
                    Name = _adminRole
                };
                await _roleManager.CreateAsync(adminRole);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            var adminUserEmail = "malik.couchy@gmail.com";
            var adminUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = adminUserEmail,
                FirstName = "Malik",
                LastName = "Couchy",
                PhoneNumber = "0678956820"
            };

            if (await _userManager.FindByEmailAsync(adminUserEmail) == null)
            {
                await _userManager.CreateAsync(adminUser);
                await _userManager.AddToRoleAsync(adminUser, _adminRole);
                adminUser.EmailConfirmed = true;
                adminUser.LockoutEnabled = false;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
