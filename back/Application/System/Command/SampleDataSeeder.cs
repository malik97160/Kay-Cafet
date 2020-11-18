using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Command
{
    public class SampleDataSeeder
    {
        private readonly IKayCafetDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _adminRole = "Administrator";
        private readonly IUserManagerService _userManagerService;

        /*TODO create a usermanagement service to handle user creation and roles*/

        public SampleDataSeeder(IKayCafetDbContext context, RoleManager<IdentityRole> roleManager, IUserManagerService userManagerService)
        {
            _context = context;
            _roleManager = roleManager;
            _userManagerService = userManagerService;
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
            var lastName = "couchy";
            var firstName = "malik";
            var adminAlreadyCreated = await _context.Users.AnyAsync(u => u.FirstName == firstName && u.LastName == lastName);
            if (adminAlreadyCreated)
                return;

            var adminUserEmail = "malik.couchy@gmail.com";
            var adminId = Guid.NewGuid().ToString();
            var adminUser = new User()
            {
                Id = adminId,
                FirstName = firstName,
                LastName = lastName
            };

            _context.Users.Add(adminUser);
            await _userManagerService.CreateUserAsync(firstName, lastName, "Azerty_971$", adminId, adminUserEmail, "0678956820");
            await _context.SaveChangesAsync(cancellationToken);
            await _userManagerService.AddRoleToUserByEmailAsync(adminUserEmail, _adminRole);
        }
    }
}
