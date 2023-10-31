using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(IBlogService blogService, UserManager<IdentityUser> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogService.GetAllBlogsAsync();

            return Ok(result);
        }

        [HttpGet("byuser")]
        public async Task<IActionResult> GetBlogsByUserId()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _blogService.GetBlogsByUserId(user.Id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var result = await _blogService.GetBlogByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDTO data)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;
            var username = user.UserName;
            var result = await _blogService.CreateBlog(userId, username, data);

            if (result.Validation.Conditions.Any())
            {
                return BadRequest(result.Validation);
            }

            return Ok(result.Commited);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(Guid blogId, UpdateBlogDTO data)
        {
            var result = await _blogService.UpdateBlog(blogId, data);

            if (result.Validation.Conditions.Any())
            {
                return BadRequest(result.Validation);
            }

            return Ok(result.Commited);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var result = await _blogService.RemoveBlog(id);

            if (result.Validation.Conditions.Any())
            {
                return BadRequest(result.Validation);
            }

            return Ok(result.Commited);
        }
    }
}
