using CityOs.FileServer.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize("ReadDocument")]
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetDocumentByNameAsync(string fileName)
        {
            var fileInformation = await _documentAppService.GetLastFileInfoVersionAsync(fileName);

            if (fileInformation?.Stream == null) return NotFound();

            return File(fileInformation.Stream, fileInformation.FileType);
        }


        [Authorize("ReadDocument")]
        [HttpGet("{fileName}/version/{version}")]
        public async Task<IActionResult> GetDocumentByVersionAsync(string fileName,int version)
        {
            var fileInformation = await _documentAppService.GetFileInfoByVersionAsync(fileName,version);

            if (fileInformation?.Stream == null) return NotFound();

            return File(fileInformation.Stream, fileInformation.FileType);
        }

        [Authorize("WriteDocument")]
        [HttpPost]
        public async Task<IActionResult> SaveDocumentAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();

            var savedFileName = await _documentAppService.SaveDocumentAsync(stream, file.FileName, file.ContentType);

            return Ok(savedFileName);
        }

        [Authorize("WriteDocument")]
        [HttpDelete("images/{imageName}")]
        public async Task<IActionResult> DeleteDocumentAsync(string imageName)
        {
            await _documentAppService.DeleteImageAsync(imageName);

            return Ok();
        }
    }
}
