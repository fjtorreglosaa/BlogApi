using BlogApi.Application.Utilities.DTOs.PostDTOs;

namespace BlogApi.Application.Utilities.DTOs.BlogDTOs
{
    public class BlogDTO
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public ICollection<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}
