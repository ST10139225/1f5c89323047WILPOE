using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KNN.Managers
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private UserManager<ApplicationUser> _userManager;

        private IBlogRepository _blogService;
        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IBlogRepository blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }




        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Blogs = _blogService.GetAllAsync(applicationUser)
            };
        }
    }
}
