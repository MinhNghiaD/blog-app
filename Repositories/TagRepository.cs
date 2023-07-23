using blog_app.Data;
using blog_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace blog_app.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _dbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Tag> ReadTagAsync(Guid tagID)
        {
            return _dbContext.Tags.FirstAsync(x => x.ID == tagID);
        }

        public async Task<int> WriteTagAsync(Tag tag)
        {
            var existingTag = await ReadTagAsync(tag.ID);
            if (existingTag == null) 
            {
                // add new tag
                await _dbContext.Tags.AddAsync(tag);
            }
            else 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
            }
            
            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<Tag>> ListTagAsync()
        {
            return _dbContext.Tags.ToListAsync();
        }

        public async Task<bool> DeleteTagAsync(Guid tagID)
        {
            var tag = await ReadTagAsync(tagID);
            if (tag != null) {
                _dbContext.Tags.Remove(tag);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}