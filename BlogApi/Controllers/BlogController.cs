using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogService.GetAllBlogsAsync();

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
            var result = await _blogService.CreateBlog(data);

            if (result.Validation.Conditions.Any())
            {
                return BadRequest(result.Validation);
            }

            return Ok(result.Commited);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, UpdateBlogDTO data)
        {
            var result = await _blogService.UpdateBlog(id, data);

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
