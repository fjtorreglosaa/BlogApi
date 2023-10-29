using BlogApi.Application.Services.Auth.Contracts;
using BlogApi.Application.Utilities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(IAuthService authSerice, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _authService = authSerice;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(UserCredsDTO credentials)
        {
            var user = new IdentityUser
            {
                UserName = credentials.Username,
                Email = credentials.Email
            };

            var resultManager = await _userManager.CreateAsync(user, credentials.Password);

            if (!resultManager.Succeeded) return BadRequest(resultManager.Errors);

            return await _authService.BuildToken(credentials); 
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(UserCredsDTO credentials)
        {
            var authResult = await _signInManager
                .PasswordSignInAsync(credentials.Email, credentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (authResult.Succeeded) return await _authService.BuildToken(credentials);

            return BadRequest("Login Failed");
        }
    }
}
