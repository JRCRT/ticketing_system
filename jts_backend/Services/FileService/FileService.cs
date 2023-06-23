using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;
using Microsoft.VisualBasic;

namespace jts_backend.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                    var filePath = Path.Combine(
                        _configuration.GetSection("AppSettings:FilePath").Value!,
                        Path.GetRandomFileName()
                    );

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
