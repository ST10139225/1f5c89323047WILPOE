using KNN.Controllers;
using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace KNN.Tests.Controllers
{
    public class AdminControllerTest
    {
        private readonly Mock<IAdminBusinessManager> _adminBusinessManagerMock;
        private readonly AdminController _controller;
        private readonly Mock<ApplicationDbContext> _contextMock;

        public AdminControllerTest()
        {
            _adminBusinessManagerMock = new Mock<IAdminBusinessManager>();
            _contextMock = new Mock<ApplicationDbContext>();


            _controller = new AdminController(_adminBusinessManagerMock.Object);
        }

        [Fact]
        public void Index_Returns_ViewResult()
        { 
            var result = _controller.Index();
             Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ManageBlogs_Returns_ViewResult_With_DashboardData()
        {
            // Arrange
            var user = new System.Security.Claims.ClaimsPrincipal();
            var expectedDashboardData = new IndexViewModel(); // Use your actual model class here
            _adminBusinessManagerMock.Setup(m => m.GetAdminDashboard(user)).ReturnsAsync(expectedDashboardData);

            // Act
            var result = await _controller.ManageBlogs();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        // Additional tests can be added for error scenarios, or edge cases
    }
}
