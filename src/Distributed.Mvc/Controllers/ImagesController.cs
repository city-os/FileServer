using CityOs.FileServer.AppService;
using CityOs.FileServer.Distributed.Mvc.Extensions;
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
            var fileInformation = await _imageAppService.GetStreamByFileNameAsync(fileName, imageQuery);
            
            if (fileInformation == null) return NotFound();

            return File(fileInformation.Stream, fileInformation.FileType);
        }

        [HttpPost]
        public async Task<IActionResult> SaveImageAsync(IFormFile image, bool isPublic = false)
        {
            var fileInformationDto = image.ToFileInfoDto();

            var savedImage = await _imageAppService.SaveImageAsync(fileInformationDto);

            return Ok(savedImage);
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteImageAsync(string fileName)
        {
            await _imageAppService.DeleteImageAsync(fileName);

            return Ok();
        }
    }
}
