using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using blog_app.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace blog_app.Controllers
{
    //[Route("[controller]")]
    public class AdminTagsController : Controller
    {
        private readonly ILogger<AdminTagsController> _logger;

        public AdminTagsController(ILogger<AdminTagsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation(message: "view tag");
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Post(AddTagRequest request)
        {
            _logger.LogInformation(message: "saving tag");
            string name = Request.Form["name"];
            string displayName = Request.Form["displayName"];

            // Render tha Add view
            return View("Add");
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}