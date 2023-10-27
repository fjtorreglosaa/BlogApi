using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
}
