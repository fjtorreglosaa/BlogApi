using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities.DTOs.PostDTOs
{
    public class CreatePostDTO
    {
        public Guid AuthorId { get; set; }
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
