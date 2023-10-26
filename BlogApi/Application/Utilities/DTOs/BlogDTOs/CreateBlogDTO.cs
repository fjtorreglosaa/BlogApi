namespace BlogApi.Application.Utilities.DTOs.BlogDTOs
{
    public class CreateBlogDTO
    {
        public Guid AuthorId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
    }
}
