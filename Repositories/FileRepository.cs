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
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(fileDirectory.FullName, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public async Task<IFormFile> LoadAsync(string url) {
            // Read the image data from the file
            byte[] imageData;
            using (var imageStream = new FileStream(url, FileMode.Open, FileAccess.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    imageStream.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            // Create an IFormFile instance
            var imageFileName = Path.GetFileName(url);
            var imageContentType = "image/jpeg"; // You should determine the content type based on the actual image type (e.g., jpeg, png, gif, etc.)

            IFormFile formFile = new FormFile(new MemoryStream(imageData), 0, imageData.Length, "LoadedImage", imageFileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = imageContentType
            };

            return formFile;
        }
    }
}