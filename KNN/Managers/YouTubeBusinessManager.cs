using KNN.Interfaces;
using KNN.Models;

namespace KNN.Managers
{
    public class YouTubeBusinessManager : IYouTubeBusinessManager
    {
        private readonly IYoutubeRepo _repo;

        public YouTubeBusinessManager(IYoutubeRepo repo)
        {
            _repo = repo;
        }

        public async Task<VideoModel> Add(VideoModel newBlog)
        {
           return await _repo.Add(newBlog);
        }

        public async Task<VideoResultModel> GetYoubeVideos()
        {
            return new VideoResultModel()
            {
                NextPageToken = string.Empty,
                PrevPageToken = string.Empty,
                Videos = _repo.GetAll()
            };

             
        }

        public bool VideoExists(string? link)
        {
            return _repo.Exists(link);
        }
    }
}
