using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Configuration;
using jts_backend.Context;
using jts_backend.Models;
using jts_backend.Services.FileService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileServie;
        private readonly JtsContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly AppSettings _settings;

        public FileController(
            IFileService fileService,
            JtsContext context,
            IWebHostEnvironment env,
            IOptions<AppSettings> settings
        )
        {
            _fileServie = fileService;
            _context = context;
            _env = env;
            _settings = settings.Value;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<FileUploadResponse>> UploadFile(IFormFile upload)
        {
            var storedFileName = Path.GetRandomFileName();

            var filePath = Path.Combine(_settings.FilePath!, "Uploads", storedFileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var response = new FileUploadResponse()
            {
                url = $"{_settings.ApiUrl}/File/GetImage?fileName={storedFileName}"
            };

            return Ok(response);
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            var uploadResult = await _context.file.FirstOrDefaultAsync(
                f => f.stored_file_name.Equals(fileName)
            );

            var path = Path.Combine(_settings.FilePath!, "Uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, uploadResult!.content_type, uploadResult.original_file_name);
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string fileName)
        {
            var path = Path.Combine(_settings.FilePath!, "Uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "image/png", "image");
        }
    }
}
