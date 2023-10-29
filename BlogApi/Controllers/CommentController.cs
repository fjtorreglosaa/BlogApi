using BlogApi.Application.Services.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using SmallBlog.Application.Services.Domain;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

    }
}
