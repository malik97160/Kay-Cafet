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
        public async Task<ActionResult<TokensVm>> Login([FromBody] LoginCredential credential)
        {
            var response = await _authService.LoginAsync(credential.userName, credential.password);
            return ReturnTokensVm(response);
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> TestAuthentication()
        {
            return Ok(new List<string>() { "Strasbourg", "Bordeaux", "Lille", "Le Man", "Paris" });
        }

        [HttpPost]
        public async Task<ActionResult<TokensVm>> Register([FromBody] UserToRegister userAuth)
        {
            var response = await _authService.RegisterAsync(userAuth);
            return ReturnTokensVm(response);
        }

        [HttpPost]
        public async Task<ActionResult<TokensVm>> RefreshToken([FromBody] TokensVm tokens)
        {
            var response = await _authService.RefreshTokenAsync(tokens.JwtToken, tokens.RefreshToken);
            return ReturnTokensVm(response);
        }
        private ActionResult<TokensVm> ReturnTokensVm(AuthenticationResult response)
        {
            if (!(response?.Success ?? false))
            {
                return Unauthorized(response.Errors);
            }
            var tokens = new TokensVm() { RefreshToken = response.RefreshToken, JwtToken = response.Token };
            return Ok(tokens);
        }

    }
}
