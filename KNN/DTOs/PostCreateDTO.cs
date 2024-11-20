using KNN.Models;

namespace KNN.DTOs
{
    public class PostCreateDTO
    { 
        public Blog blog { get; set; }
        public ApplicationUser user { get; set; }
        public Post Parent { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
