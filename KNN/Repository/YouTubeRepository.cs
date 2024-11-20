using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KNN.Repository
{
    public class YouTubeRepository : IYoutubeRepo
    {
        private readonly ApplicationDbContext _context; 

        public YouTubeRepository(ApplicationDbContext context)
        {
            _context  = context;
        }
         

        public async Task<VideoModel> Add(VideoModel newVideoModel)
        {
            await _context.Videos.AddAsync(newVideoModel);
             await _context.SaveChangesAsync();
            return newVideoModel;
        } 

        public   IEnumerable<VideoModel> GetAll()
        {
              return   _context.Videos.ToList();
        }

        public IEnumerable<VideoModel> GetAllAsyncHome()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VideoModel> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Exists(string? id)
        {
            return _context.Videos.Any(x => x.Link == id);
             
        }
    }
}
