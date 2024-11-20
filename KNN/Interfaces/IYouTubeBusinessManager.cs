using KNN.Models;
using System.Security.Claims;

namespace KNN.Interfaces
{
    public interface IYouTubeBusinessManager
    {
        public Task<VideoResultModel> GetYoubeVideos();
        public bool VideoExists(string? link);

        public Task<VideoModel> Add(VideoModel newBlog);
    }
}
