using Microsoft.AspNetCore.Mvc.Rendering;

namespace blog_app.Models.Views
{
    public class EditBlogPostRequest
    {
        public Guid ID { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public IFormFile FeatureImageFile { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public List<Guid> SelectedTagIDs { get; set; }
    }
}