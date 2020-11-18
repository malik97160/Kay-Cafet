using Application.Common.Interfaces;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddRoleToUserByEmailAsync(string email, string adminRole)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), email);
            await _userManager.AddToRoleAsync(user, adminRole);
        }

        public async Task CreateUserAsync(string firstName, string lastName, string password, string userId, string email, string phoneNumber)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                throw new AlreadyCreatedException(nameof(ApplicationUser), email);
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = (firstName + lastName).ToLower(),
                Email = email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = phoneNumber 
            };
            await _userManager.CreateAsync(user, password);
        }
    }
}
