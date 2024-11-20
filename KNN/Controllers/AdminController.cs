using KNN.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KNN.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManager _adminBusinessManager;

        public AdminController(IAdminBusinessManager adminBusinessManager)
        {
            _adminBusinessManager = adminBusinessManager;
        }

        public IActionResult Index() {

            return View();
        }
        public async Task<IActionResult> ManageBlogs()
        {
            return View(await _adminBusinessManager.GetAdminDashboard(User));
        }


    }
}
