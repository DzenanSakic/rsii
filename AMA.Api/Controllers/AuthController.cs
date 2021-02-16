using AMA.Api.JWT;
using AMA.Repositories.Interfaces;
using AMA.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMA.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IRepositoryUser _repositoryUser;

        public AuthController(IUserService userService, 
            IJwtAuthManager jwtAuthManager,
            IRepositoryUser repositoryUser)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _repositoryUser = repositoryUser;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
            {
                return Unauthorized();
            }

            var user = _repositoryUser.TryFind(request.UserName);
            var role = _userService.GetUserRole(request.UserName);

            if(user.Status == Common.Enumerations.UserStatus.Blocked)
            {
                return BadRequest("User is blocked");
            }

            var claims = new[]
            {
            new Claim("Id", user.ID.ToString()),
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role, role.Role.ToString())
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = role.Role.ToString(),
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString,
                Id = user.ID
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName); // can be more specific to ip, user agent, device name, etc.
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }
    }
}
