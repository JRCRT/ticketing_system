using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Services.FileService
{
    public interface IFileService
    {
        public Task<FileUploadResponse> UploadFiles(IFormFile file);
    }
}
