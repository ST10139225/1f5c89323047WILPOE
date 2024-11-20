namespace KNN.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public ApplicationUser? Creator { get; set; }

        public string  Title { get; set; }
        public string  Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool Published { get; set; }


        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
