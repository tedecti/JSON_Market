using JSON_Market.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace JSON_Market.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IFileRepository _fileRepository;

        public FilesController(IWebHostEnvironment environment, IFileRepository fileRepository)
        {
            _environment = environment;
            _fileRepository = fileRepository;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Index(string fileName)
        {
            var imageStream = await _fileRepository.GetFile(fileName);
            if (imageStream == null)
                return NotFound();

            using (var image = Image.Load(imageStream))
            {
                image.Mutate(x => x
                    .Resize(new ResizeOptions
                    {
                        Size = new Size(375, 500),
                        Mode = ResizeMode.Max
                    })
                );

                var compressedImageStream = new MemoryStream();
                image.Save(compressedImageStream, new PngEncoder());
                compressedImageStream.Seek(0, SeekOrigin.Begin);

                return File(compressedImageStream, "image/png");
            }
        }
    }
}