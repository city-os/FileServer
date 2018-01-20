using CityOs.FileServer.AppService;
using CityOs.FileServer.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CityOs.FileServer.Distributed.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        /// <summary>
        /// The service who manage images
        /// </summary>
        private readonly IImageAppService _imageAppService;

        /// <summary>
        /// Initialize a default <see cref="ImagesController"/>
        /// </summary>
        /// <param name="imageAppService">The service who manage images</param>
        public ImagesController(IImageAppService imageAppService)
        {
            _imageAppService = imageAppService;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetImageByNameAsync(string fileName, [FromQuery] ImageQueryDto imageQuery)
        {
            var stream = await _imageAppService.GetStreamByFileNameAsync(fileName, imageQuery);

            if (stream == null) return NotFound();

            return File(stream, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(IFormFile image, bool isPublic = false)
        {
            var savedImage = await _imageAppService.SaveImageAsync(image.OpenReadStream(), image.FileName, image.ContentType);

            return Ok(savedImage);
        }
    }
}
