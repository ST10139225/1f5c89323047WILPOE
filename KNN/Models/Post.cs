using Google.Apis.YouTube.v3.Data;

namespace KNN.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Blog blog { get; set; }
        public ApplicationUser poser { get; set; }
        public string  Content{ get; set; }
        public Post Parent { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual IEnumerable<Post> Comments { get; set; }
    }
}
