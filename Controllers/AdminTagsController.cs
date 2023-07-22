using blog_app.Models.Domain;
using blog_app.Models.Views;
using blog_app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace blog_app.Controllers
{
    //[Route("[controller]")]
    public class AdminTagsController : Controller
    {
        private readonly ILogger<AdminTagsController> _logger;
        private readonly ITagRepository _repository;

        public AdminTagsController(ILogger<AdminTagsController> logger, ITagRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            Tag newTag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await _repository.WriteTagAsync(newTag);

            // Render tha Add view
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {   
            var tags = await _repository.ListTagAsync();
            return View(tags);
        }

        [HttpGet("AdminTags/Edit/{tagID}")]
        public async Task<IActionResult> Edit(Guid tagID)
        {   
            _logger.LogInformation("Editing tag with ID " + tagID);
            // retrieve tag with tagID received from route
            var tag = await _repository.ReadTagAsync(tagID);
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

            Tag tag = new Tag
            {
                ID = request.ID,
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await _repository.WriteTagAsync(tag);

            return RedirectToAction("List");
        }

        [HttpPost("AdminTags/Delete/{tagID}")]
        public async Task<IActionResult> Delete(Guid tagID) {
            _logger.LogInformation("Deleting tag " + tagID);

            await _repository.DeleteTagAsync(tagID);

            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}