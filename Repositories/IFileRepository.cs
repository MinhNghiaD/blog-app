using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_app.Repositories
{
    public interface IFileRepository
    {
        public Task<string> UploadAsync(IFormFile file);
    }
}