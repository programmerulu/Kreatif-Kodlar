using BlogProject.Data;
using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostRepository _postsRepository;

        public HomeController(ILogger<HomeController> logger,PostRepository postRepository)
        {
            _logger = logger;
            _postsRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postsRepository.GetAllAsync();
            return View(posts);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
