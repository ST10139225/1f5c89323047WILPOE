namespace KNN.Models
{
    public class VideoResultModel
    {
        public IEnumerable<VideoModel> Videos { get; set; }
        public string? NextPageToken{ get; set; }
        public string? PrevPageToken{ get; set; }
    }
}
