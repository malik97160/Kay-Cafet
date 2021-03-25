using Common.Exceptions;
using Infrastructure.Authentication;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webUI.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string userName, string password)
        {
            var response = await _authService.LoginAsync(userName, password);
            return (response?.Success ?? false) ? Ok(response.Token) : Unauthorized(response.Errors);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> TestAuthentication()
        {
            return Ok(new List<string>() { "Strasbourg", "Bordeaux", "Lille", "Le Man", "Paris" });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserToRegister userAuth)
        {
            try
            {
                var token = await _authService.Register(userAuth);
                return Ok(token);
            }
            catch (AuthenticationException e)
            {

                return BadRequest(new { Error = e.Errors });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
        {
            var response = await _authService.RefreshTokenAsync(refreshToken.JwtToken, refreshToken.RefreshToken);
            return (response?.Success ?? false) ? Ok(response.Token) : Unauthorized(response.Errors);
        }
    }
}
