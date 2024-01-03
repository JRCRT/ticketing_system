using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Configuration;
using jts_backend.Context;
using jts_backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace jts_backend.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly JtsContext _context;
        private readonly AppSettings _settings;

        public FileService(
            IWebHostEnvironment env,
            JtsContext context,
            IOptions<AppSettings> settings
        )
        {
            _env = env;
            _context = context;
            _settings = settings.Value;
        }

        public async Task<FileUploadResponse> UploadFiles(IFormFile upload)
        {
            var storedFileName = Path.GetRandomFileName();

            var filePath = Path.Combine(_settings.FilePath!, "Uploads", storedFileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var response = new FileUploadResponse() { url = filePath };

            return response;
        }
    }
}
