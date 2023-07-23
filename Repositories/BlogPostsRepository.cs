using blog_app.Data;
using blog_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace blog_app.Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        public Task<BlogPost?> ReadBlogPostAsync(Guid ID)
        {
            return _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == ID);
        }
        private readonly BlogDbContext _dbContext;

        public BlogPostsRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> WriteBlogPostAsync(BlogPost blogPost)
        {
            var existingblogPost = await ReadBlogPostAsync(blogPost.ID);
            if (existingblogPost == null) 
            {
                // add new blog post
                await _dbContext.BlogPosts.AddAsync(blogPost);
            }
            else 
            {
                existingblogPost.Heading = blogPost.Heading;
                existingblogPost.PageTitle = blogPost.PageTitle;
                existingblogPost.Content = blogPost.Content;
                existingblogPost.ShortDescription = blogPost.ShortDescription;
                existingblogPost.FeatureImageUrl = blogPost.FeatureImageUrl;
                existingblogPost.UrlHandle = blogPost.UrlHandle;
                existingblogPost.PublishDate = blogPost.PublishDate;
                existingblogPost.Author = blogPost.Author;
                existingblogPost.Visible = blogPost.Visible;
                existingblogPost.Tags = blogPost.Tags;
            }

            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<BlogPost>> ListBlogPostsAsync()
        {
            return _dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<bool> DeleteBlogPostAsync(Guid ID)
        {
            var blogPost = await ReadBlogPostAsync(ID);
            if (blogPost != null) {
                _dbContext.BlogPosts.Remove(blogPost);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}