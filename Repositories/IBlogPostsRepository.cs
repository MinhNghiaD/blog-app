using blog_app.Models.Domain;

namespace blog_app.Repositories
{
    public interface IBlogPostsRepository
    {
        public Task<BlogPost?> ReadBlogPostAsync(Guid ID);
        public Task<int> WriteBlogPostAsync(BlogPost blogPost);
        public Task<List<BlogPost>> ListBlogPostsAsync();
        public Task<bool> DeleteBlogPostAsync(Guid ID);
    }
}