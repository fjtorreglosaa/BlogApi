namespace BlogApi.Domain.Models
{
    public class Post : Model
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid AuthorId { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
