using blog_app.Models.Domain;
using blog_app.Models.Views;
using blog_app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace blog_app.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ILogger<BlogPostsController> _logger;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly ITagRepository _tagRepository;

        public BlogPostsController(ILogger<BlogPostsController> logger, IBlogPostsRepository blogPostsRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostsRepository = blogPostsRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<Tag> tags = await _tagRepository.ListTagAsync();
            List<SelectListItem> selectableTags = new List<SelectListItem>();

            foreach(Tag tag in tags) {
                selectableTags.Add(new SelectListItem
                {
                    Text = tag.DisplayName,
                    Value = tag.ID.ToString(),
                    Selected = false
                });
            }

            AddBlogPostRequest request = new AddBlogPostRequest
            {
                Tags = selectableTags
            };

            return View(request);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {
            BlogPost blogPost = new BlogPost
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeatureImageUrl = request.FeatureImageUrl,
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                Visible = request.Visible,
                Tags = new List<Tag>()
            };

            foreach (Guid tagID in request.SelectedTagIDs) {
                var tag = await _tagRepository.ReadTagAsync(tagID);
                if (tag != null) 
                {
                    blogPost.Tags.Add(tag);
                }
            }

            await _blogPostsRepository.WriteBlogPostAsync(blogPost);
            // Render the Add view
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {   
            var blogPosts = await _blogPostsRepository.ListBlogPostsAsync();
            return View(blogPosts);
        }
    }
}