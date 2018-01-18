using CityOs.FileServer.AppService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CityOs.FileServer.Distributed.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        /// <summary>
        /// The application service who manage documents
        /// </summary>
        private readonly IDocumentAppService _documentAppService;

        /// <summary>
        /// Initialize a default <see cref="DocumentsController"/>
        /// </summary>
        /// <param name="documentAppService">The service who manage the documents</param>
        public DocumentsController(IDocumentAppService documentAppService)
        {
            _documentAppService = documentAppService;
        }

        [HttpGet("images/{imageName}")]
        public async Task<IActionResult> GetImageByIdentifierAsync(string imageName)
        {
            var stream = await _documentAppService.GetImageStreamByIdentifierAsync(imageName);

            if (stream == null) return NotFound();

            return File(stream, "image/jpeg");
        }

        [HttpPost("images")]
        public async Task<IActionResult> SaveImageAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();

            var savedFileName = await _documentAppService.SaveImageAsync(stream, file.FileName, file.ContentType);

            return Ok(savedFileName);
        }

        [HttpDelete("images/{imageName}")]
        public async Task<IActionResult> DeleteImageAsync(string imageName)
        {
            await _documentAppService.DeleteImageAsync(imageName);

            return Ok();
        }
    }
}
