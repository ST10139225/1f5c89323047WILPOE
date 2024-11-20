using KNN.Interfaces;
using KNN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KNN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogBusinessManager _blogBusinessManager;


        public HomeController(ILogger<HomeController> logger,IBlogBusinessManager blogBusinessManager)
        {
            _logger = logger;
            _blogBusinessManager = blogBusinessManager;
        }

        public IActionResult Index()
        {
            return View(  );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Episodes()
        {
            return View(await _blogBusinessManager.GetEpisodes());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About() { 
        
        return View();
        }
    }
}
