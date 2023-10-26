namespace BlogApi.Domain.Models
{
    public class Blog : Model
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
