using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuiqBlog.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {

        private readonly IBlogBusinessManager blogBusinessManager;
        private readonly IAdminBusinessManager _adminBusinessManager;


        public BlogsController(IBlogBusinessManager blogBusinessManager, IAdminBusinessManager adminBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
            _adminBusinessManager = adminBusinessManager;
        }

        [Route("Post/{id}"), AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
                return NotFound("Blog ID is required.");

            var actionResult = await blogBusinessManager.GetBlogViewModel(id.Value, User);

            if (actionResult?.Value is null)
                return NotFound("Blog post not found.");

            if (actionResult.Result != null)
                return actionResult.Result;

            return View(actionResult.Value);
        }


        public IActionResult Create()
        {
            return View(new BlogViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogViewModel createBlogViewModel)
        {
            if (createBlogViewModel is null)
            {
                throw new ArgumentNullException(nameof(createBlogViewModel));
            }

            await blogBusinessManager.CreateBlog(createBlogViewModel, User);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var actionResult = await blogBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await blogBusinessManager.UpdateBlog(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.blog.Id });

            return actionResult.Result;
        }

        //Comment section
        [HttpPost]
        public async Task<IActionResult> Comment(BlogPostVM blogPostVM)
        {
            if (blogPostVM == null)
                return BadRequest("Comment data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actionResult = await blogBusinessManager.CreateComment(blogPostVM, User);

            if (actionResult.Result != null)
                return actionResult.Result;

            return RedirectToAction("Index", new { id = blogPostVM.Blog.Id });
        }



        public async Task<IActionResult> Delete(int? id)
        {
             await blogBusinessManager.deleteEpisode(id);

           

            return RedirectToAction("ManageBlogs","Admin");
        }
    }
}