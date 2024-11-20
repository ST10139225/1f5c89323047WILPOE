using KNN.Models;

namespace KNN.DTOs
{
    public class BlogCreateDTO
    {  

        public string  Title { get; set; }
        public string  Content { get; set; } 

        public bool Published { get; set; }


        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
