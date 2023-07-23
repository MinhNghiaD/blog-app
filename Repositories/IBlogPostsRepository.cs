using blog_app.Models.Domain;

namespace blog_app.Repositories
{
    public interface IBlogPostsRepository
    {
        public Task<int> WriteBlogPostAsync(BlogPost blogPost);
        public Task<List<BlogPost>> ListBlogPostsAsync();
    }
}