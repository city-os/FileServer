using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CityOs.FileServer.Core
{
    public class DocumentController : Controller
    {
        //{
        //    [HttpPost]
        //    public async Task<IActionResult> Index()
        //    {
        //        FormValueProvider formModel;
        //        using (var stream = System.IO.File.Create("c:\\temp\\myfile.temp"))
        //        {
        //            formModel = await Request.StreamFile(stream);
        //        }

        //        var viewModel = new MyViewModel();

        //        var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
        //            valueProvider: formModel);

        //        if (!bindingSuccessful)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }
        //        }

        //        return Ok(viewModel);
        //    }
        //    }
        //public class MyViewModel
        //{
        //}

        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }


        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            using (var readStream = file.OpenReadStream())
            {
               await _documentRepository.InsertDocumentAsync(readStream, file.ContentType, file.Name);
            }

            return Ok(new {count = files.Count});

        }
    }
}

