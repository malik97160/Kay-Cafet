using Application.Common.Interfaces;
using Common.Exceptions;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;
        private readonly IKayCafetDbContext _context;

        //private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, TokenValidationParameters tokenValidationParameters, IConfiguration configuration, IKayCafetDbContext context)
        {
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return await GenerateAccessToken(user);
            }

            return new AuthenticationResult() { Success = false };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string jwtToken, string refreshToken)
        {
            var validatedToken = GetPrincipalFromExpiredToken(jwtToken);
            if (validatedToken == null)
            {
                return new AuthenticationResult() { Errors = new[] { "Invalid token" } };
            }

            var expiryDateUnix = long.Parse(validatedToken
                .Claims
                .Single(t => t.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateUtc =
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult() { Errors = new[] { "The token has not been expired yet." } };
            }

            var jti = validatedToken.Claims
                .Single(t => t.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(r => r.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult() { Errors = new[] { "The refresh token is null" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult() { Errors = new[] { "The refresh token is expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult() { Errors = new[] { "The refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult() { Errors = new[] { "The refresh token has been used." } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult() { Errors = new[] { "The refresh token does not match this JWT" } };
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);

            await _context.SaveChangesAsync(new System.Threading.CancellationToken());

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(t => t.Type == "UserId").Value);

            return await GenerateAccessToken(user);

        }

        public async Task<AuthenticationResult> RegisterAsync(UserToRegister userToRegister)
        {
            if (string.IsNullOrWhiteSpace(userToRegister.email))
                throw new AuthenticationException(new List<string>() { "email should not be null nor empty" });

            if (string.IsNullOrWhiteSpace(userToRegister.userName))
                throw new AuthenticationException(new List<string>() { "userName should not be null nor empty" });

            var existingUser = await _userManager.FindByNameAsync(userToRegister.userName);
            if (existingUser != null)
            {
                throw new AuthenticationException(new List<string>() { $"User with userName {userToRegister.userName} already exists" });
            }

            var user = new ApplicationUser() { UserName = userToRegister.userName, Email = userToRegister.email };
            var result = await _userManager.CreateAsync(user, userToRegister.password);
            if (!result.Succeeded)
                throw new AuthenticationException(result.Errors.Select(x => x.Description).ToList());

            await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new Claim("email", user.Email));

            return await GenerateAccessToken(user);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtEncryptedWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch (Exception)
            {

                return null;
            }
        }

        private async Task<AuthenticationResult> GenerateAccessToken(ApplicationUser user)
        {
            var now = DateTime.UtcNow;
            var succeeded = double.TryParse(_configuration["Jwt:TokenLifeTime"], out var tokenLifeTime);
            if (!succeeded)
            {
                tokenLifeTime = 1;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var jwtToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserId", user.Id),
                    }),
                Expires = now.AddMinutes(tokenLifeTime),
                NotBefore = now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Issuer"],
                Issuer = _configuration["Jwt:Issuer"],
            };
            var token = tokenHandler.CreateToken(jwtToken);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync(new CancellationToken());

            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private bool IsJwtEncryptedWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg
                    .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
