namespace blog_app.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DirectoryInfo fileDirectory;

        public FileRepository() 
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "data", "images");
            // Create the folder if it doesn't exist
            fileDirectory = Directory.CreateDirectory(uploadsFolder);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(fileDirectory.FullName, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}