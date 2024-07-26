using WalkAPI.Data;
using WalkAPI.Models.Domain;

namespace WalkAPI.Responsitories
{
    public class LocalImageResponsitory : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalkDbContext dbContext;

        public LocalImageResponsitory(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalkDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            
            //https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" + //https
                $"://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" + //localhost
                $"{httpContextAccessor.HttpContext.Request.PathBase}" + //local file past
                $"/Images/" + 
                $"{image.FileName}{image.FileExtension}"; // image file + extension

            image.FilePath = urlFilePath;

            //Add image to Image Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            
            return image;
        }
    }
}
