using Google;
using Google.Apis.YouTube.v3.Data;
using KNN.Authorization;
using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PageList;
using System.Security.Claims;

namespace KNN.Services
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogRepository _blogRepository;

        //For uploading a picture
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthorizationService _authorizationService;


        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogRepository blogRepository, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _blogRepository = blogRepository;
            _webHostEnvironment = webHostEnvironment;
            _authorizationService = authorizationService;
        }

        ////Pagination
        //public HomeIndexViewModel GetIndexViewModel(string searchString, int? page)
        //{
        //    int pageSize = 2;
        //    int pageNumber = page ?? 1;
        //    var blogs = _blogRepository.GetBlog(searchString ?? string.Empty)
        //        .Where(blog => blog.Published);
        //    return new HomeIndexViewModel
        //    {
        //        Blogs = new  PageList<Blog>(blogs.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, blogs.Count()),
        //        SearchString = searchString,
        //        PageNumber = pageNumber
        //    };
        //}


        public async Task<Blog> CreateBlog(BlogViewModel blogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            
                Blog newBlog = blogViewModel.blog;

                newBlog.Creator = await _userManager.GetUserAsync(claimsPrincipal);
                newBlog.CreatedOn = DateTime.UtcNow;
                newBlog.UpdatedOn = DateTime.UtcNow;

                newBlog = await _blogRepository.Add(newBlog);

                string webRootPath = _webHostEnvironment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{newBlog.Id}\HeaderImage.jpg";

                EnsureFolder(pathToImage);
             
                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await blogViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }

                return newBlog;
            }
       

        //To ensure  the image/video file exists
        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }

        ///Editing 
        ///


        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var blog = _blogRepository.GetBlog(editViewModel.blog.Id);

            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await _authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            blog.Published = editViewModel.blog.Published;
            blog.Title = editViewModel.blog.Title;
            blog.Content = editViewModel.blog.Content;
            blog.UpdatedOn = DateTime.UtcNow;

            if (editViewModel.BlogHeaderImage != null)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await editViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                blog = await _blogRepository.Update(blog)
            };
        }
        public async Task<ActionResult<BlogPostVM>> GetBlogViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;

            var blog = _blogRepository.GetBlog(postId);

            if (blog is null)
                return new NotFoundResult();

            if (!blog.Published)
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Read);

                if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);
            }

            return new BlogPostVM
            {
                Blog = blog
            };
        }
        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;

            var blog = _blogRepository.GetBlog(blogId);

            if (blog is null)
                return new NotFoundResult();

            var authorizationResult = await _authorizationService.AuthorizeAsync(claimsPrincipal, blog, Operations.Update);

            if (!authorizationResult.Succeeded) return DetermineActionResult(claimsPrincipal);

            return new EditViewModel
            {
                blog = blog
            };
        }

        private ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        public async Task<IndexViewModel> GetEpisodes( )
        { 
            return new IndexViewModel
            {
                Blogs = _blogRepository.GetAllAsyncHome()
            };
        }
      

        public async Task deleteEpisode(int? id)
        {
           await _blogRepository.DeleteBlog(id);


        }


        // Posts funcitonality
        public async Task<ActionResult<Post>> CreateComment(BlogPostVM postViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (postViewModel.Blog is null || postViewModel.Blog.Id == 0)
                return new BadRequestResult();

            var blog = _blogRepository.GetBlog(postViewModel.Blog.Id);

            if (blog is null)
                return new NotFoundResult();

            var comment = postViewModel.Comment;

            comment.poser = await _userManager.GetUserAsync(claimsPrincipal);
            comment.blog = blog;
            comment.CreatedOn = DateTime.Now;

            if (comment.Parent != null)
            {
                comment.Parent = _blogRepository.GetComment(comment.Parent.Id);
            }

            return await _blogRepository.Add(comment);
        }


        public Post GetPost(int commentId)
        {
            return _blogRepository.GetComment(commentId);
        }
    }
}
