using blog_app.Data;
using blog_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace blog_app.Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private readonly BlogDbContext _dbContext;

        public BlogPostsRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> WriteBlogPostAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<BlogPost>> ListBlogPostsAsync()
        {
            return _dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }
    }
}