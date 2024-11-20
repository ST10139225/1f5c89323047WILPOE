using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using KNN.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KNN.Controllers
{
  
    public class YoutubeControllerTest : Controller 
    {

        private readonly Mock<IYouTubeBusinessManager> _youtubeBusinessManager;
        private readonly YoutubeController _controller;

        public YoutubeControllerTest( )
        {
            _youtubeBusinessManager = new Mock<IYouTubeBusinessManager>();
            _controller = new YoutubeController(_youtubeBusinessManager.Object);
        }


        [Fact]
        public async Task Index_returns_View_Result()
        { 
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);

        } 
    }
}
