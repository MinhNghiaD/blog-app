namespace blog_app.Models.Views
{
    public class EditTagRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}