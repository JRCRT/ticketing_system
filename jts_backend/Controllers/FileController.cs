using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;
using jts_backend.Services.FileService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileServie;

        public FileController(IFileService fileService)
        {
            _fileServie = fileService;
        }

        [HttpPost("UploadFiles")]
        public async Task<ActionResult<ServiceResponse<ICollection<string>>>> UploadFiles(
            List<IFormFile> files
        )
        {
            var response = await _fileServie.UploadFiles(files);
            return Ok(response);
        }
    }
}
