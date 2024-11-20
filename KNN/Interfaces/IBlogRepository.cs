using KNN.DTOs;
using KNN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KNN.Interfaces
{
    public interface IBlogRepository
    {
        public IEnumerable<Blog> GetAllAsync(ApplicationUser applicationUser);
        public IEnumerable<Blog> GetAllAsync(string searchString);
        public IEnumerable<Blog> GetAllAsyncHome();
        public Task<Blog> Add(Blog newBlog); 
        public Task<Blog> Update(Blog newBlog); 
        public Task<Blog> GetById(int? id);
        public Task  DeleteBlog(int? id);
        public  Blog  GetBlog(int? id);
        public Task SaveChangesAsync();

        public Task<Post> Add(Post comment);
        public Post GetComment(int commentId);

    }
}
