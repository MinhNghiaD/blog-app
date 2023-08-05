using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blog_app.Models;
using blog_app.Models.Views;
using blog_app.Repositories;

namespace blog_app.Controllers {
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogPostsRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostsRepository = blogPostsRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeView = new HomeViewModel 
            {
                BlogPosts = await _blogPostsRepository.ListBlogPostsAsync(),
                Tags = await _tagRepository.ListTagAsync()
            };

            return View(homeView);
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
