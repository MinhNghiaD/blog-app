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
    }
}