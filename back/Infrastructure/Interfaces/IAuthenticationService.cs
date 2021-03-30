using Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string userName, string password);

        Task<AuthenticationResult> RegisterAsync(UserToRegister userToRegister);

        Task<AuthenticationResult> RefreshTokenAsync(string jwtToken, string refreshToken);
    }
}
