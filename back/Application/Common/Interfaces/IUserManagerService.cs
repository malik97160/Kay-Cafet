using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserManagerService
    {
        public Task CreateUserAsync(string firstName, string lastName, string password, string userId, string email, string phoneNumber);
        public Task AddRoleToUserByEmailAsync(string email, string adminRole);
    }
}
