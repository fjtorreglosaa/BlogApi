using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public bool Edited { get; set; }
        public DateTime? LastModificated { get; set; }
    }
}
