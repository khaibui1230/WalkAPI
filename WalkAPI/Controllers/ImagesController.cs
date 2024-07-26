using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkAPI.Models.Domain;
using WalkAPI.Models.DTO;
using WalkAPI.Responsitories;

namespace WalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);
            if (ModelState.IsValid)
            {
                // Convert DTO to Domain model 
                var imageDomainModel = new Image
                {
                    File = requestDto.File,
                    FileName = Path.GetFileNameWithoutExtension(requestDto.File.FileName),
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileDescription = requestDto.FileDescription
                };

                var uploadedImage = await imageRepository.Upload(imageDomainModel);
                return Ok(uploadedImage);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            // Check the file extension
            var fileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            // Check the file size
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller file size");
            }
        }
    }
}
