using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace QuiqBlog.Controllers
{

    public class BlogsControllerTest : Controller
    {

        private readonly Mock<IBlogBusinessManager> blogBusinessManager;
        private readonly Mock<IAdminBusinessManager> _adminBusinessManager;
        private readonly BlogsController _controller;



        public BlogsControllerTest(IBlogBusinessManager blogBusinessManager, IAdminBusinessManager adminBusinessManager)
        {
            this.blogBusinessManager = new Mock<IBlogBusinessManager>();
            _adminBusinessManager = new Mock<IAdminBusinessManager>();
            _controller = new BlogsController(this.blogBusinessManager.Object,_adminBusinessManager.Object);
        }
         [Fact]
        public void Index_returns_null_when_there_are_blogs()
        {


            var actionResult = _controller.Index(1);

            Assert.Null(actionResult);

        }

        [Fact]
        public void Create()
        {
            var actionResult = _controller.Create();
            Assert.IsType<ViewResult>(actionResult);    

        }
    }
}