using KNN.Models;
using Microsoft.AspNetCore.Mvc;

namespace KNN.Interfaces
{
    public interface IYoutubeRepo
    {
        IEnumerable<VideoModel> GetAll();  
        Task<VideoModel> Add(VideoModel newVideoModel);

        bool Exists(string? id); 
    }
}
