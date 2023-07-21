using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using blog_app.Data;
using blog_app.Models.Domain;
using blog_app.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog_app.Controllers
{
    //[Route("[controller]")]
    public class AdminTagsController : Controller
    {
        private readonly ILogger<AdminTagsController> _logger;
        private readonly BlogDbContext _dbContext;

        public AdminTagsController(ILogger<AdminTagsController> logger, BlogDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Post(AddTagRequest request)
        {
            Tag newTag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await _dbContext.Tags.AddAsync(newTag);
            await _dbContext.SaveChangesAsync();

            // Render tha Add view
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {   
            var tags = await _dbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet("AdminTags/Edit/{tagID}")]
        public async Task<IActionResult> Edit(Guid tagID)
        {   
            _logger.LogInformation("Editing tag with ID " + tagID);
            // retrieve tag with tagID received from route
            var tag = await _dbContext.Tags.FindAsync(tagID);
            if (tag == null) {
                return View(null);
            }

            EditTagRequest request = new EditTagRequest
            {
                ID = tag.ID,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return View(request);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest request) {
            _logger.LogInformation("Save tag " + request.ID + " " + request.Name);

            var tag = await _dbContext.Tags.FindAsync(request.ID);
            if (tag != null) {
                tag.Name = request.Name;
                tag.DisplayName = request.DisplayName;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost("AdminTags/Delete/{tagID}")]
        public async Task<IActionResult> Delete(Guid tagID) {
            _logger.LogInformation("Deleting tag " + tagID);

            var tag = await _dbContext.Tags.FindAsync(tagID);
            if (tag != null) {
                _dbContext.Tags.Remove(tag);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}