namespace blog_app.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DirectoryInfo fileDirectory;

        public FileRepository() 
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "storage", "images");
            // Create the folder if it doesn't exist
            fileDirectory = Directory.CreateDirectory(uploadsFolder);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            string uri = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(fileDirectory.FullName, uri);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uri;
        }

        public async Task<byte[]> GetAsync(string uri)
        {
            string filePath = Path.Combine(fileDirectory.FullName, uri);
            byte[] imageData;
            using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    imageStream.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            // Create an IFormFile instance
            return imageData;
        }
    }
}