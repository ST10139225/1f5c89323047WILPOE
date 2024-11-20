using KNN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KNN.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlog(BlogViewModel blogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<IndexViewModel> GetEpisodes();
        Task deleteEpisode(int? id);
        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<BlogPostVM>> GetBlogViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<Post>> CreateComment(BlogPostVM postViewModel, ClaimsPrincipal claimsPrincipal);

    }
}
