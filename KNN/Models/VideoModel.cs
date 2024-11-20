namespace KNN.Models
{
    public class VideoModel
    {
        public int Id { get; set; } 
        public string? Title{ get; set; }
        public string? Link{ get; set; }
        public string? Thumbnail{ get; set; }
        public DateTimeOffset? PublishedDate { get; set; }
    }
}
