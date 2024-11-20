using KNN.Interfaces;
using KNN.Models;
using KNN.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace KNN.Controllers
{
    public class HomeControllerTest : Controller
    { 
        private readonly Mock<IBlogBusinessManager> _blogBusinessManager;
        private readonly HomeController _homeController;
        private readonly Mock<ILogger<HomeController>> _logger;



        public HomeControllerTest()
        {
            _logger = new Mock<ILogger<HomeController>>();
            _blogBusinessManager = new Mock<IBlogBusinessManager>();
            _homeController = new HomeController(_logger.Object,_blogBusinessManager.Object);
        }



        [Fact]
        public void Index_returns_ViewResult()
        {
            var result =  _homeController.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Privacy_returns_ViewResult()
        {
            var result = _homeController.Privacy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        } 

        [Fact]
        public void About_returns_ViewResult() {

            var result = _homeController.About();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_returns_not_null()
        {
            var result =  _homeController.Index();

            Assert.NotNull(result);

        }

        [Fact]
        public void Privacy_returns_not_null()
        {
            var result = _homeController.Privacy();

            Assert.NotNull(result);
        } 

        [Fact]
        public void About_returns_not_null() {

            var result = _homeController.About();

            Assert.NotNull(result);
        }


    }
}
