using blog_app.Models.Domain;

namespace blog_app.Repositories
{
    public interface ITagRepository
    {
        public Task<Tag> ReadTagAsync(Guid tagID);
        public Task<int> WriteTagAsync(Tag tag);
        public Task<List<Tag>> ListTagAsync();
        public Task<bool> DeleteTagAsync(Guid tagID);
    }
}