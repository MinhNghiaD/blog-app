using blog_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace blog_app.Data
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext(DbContextOptions options): base(options)
        {
        }

        // Dbset corresponds to a table in database
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}