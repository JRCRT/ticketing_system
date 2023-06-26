using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Context;
using jts_backend.Models;
using Microsoft.VisualBasic;

namespace jts_backend.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly JtsContext _context;

        public FileService(IWebHostEnvironment env, JtsContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<ServiceResponse<ICollection<string>>> UploadFiles(
            ICollection<IFormFile> files
        )
        {
            // var filePaths = new Collection<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var storedFileName = Path.GetRandomFileName();
                    var originalFileName = formFile.FileName;
                    var contentType = formFile.ContentType;

                    var filePath = Path.Combine(_env.ContentRootPath, storedFileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            var response = new ServiceResponse<ICollection<string>>() { };

            return response;
        }
    }
}
