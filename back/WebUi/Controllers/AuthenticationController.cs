using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace webUI.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && (await _userManager.CheckPasswordAsync(user, password)))
            {
                var token = generateJwtToken(user);
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> TestAuthentication()
        {
            return Ok(new List<string>() { "Strasbourg", "Bordeaux", "Lille", "Le Man", "Paris" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] UserToRegister userAuth)
        {
            var user = new ApplicationUser() { UserName = userAuth.userName, Email = userAuth.email };
            var result = await _userManager.CreateAsync(user, userAuth.password);
            if (result == null)
                return BadRequest();

            await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
            return Ok(result);
        }
        private string generateJwtToken(ApplicationUser user)
        {
            var now = DateTime.UtcNow;

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new Dictionary<string, object>(){
                    { "userId", user.Id },
                    { "userName", user.UserName }
                };
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var jwtToken = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(claims),
                Expires = now.AddDays(7),
                NotBefore = now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Claims = claims
            };
            var token = tokenHandler.CreateToken(jwtToken);
            return tokenHandler.WriteToken(token);
        }

        public record UserToRegister(string userName, string password, string email);
    }
}
