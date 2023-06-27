using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Dtos.FileDto
{
    public class CreateFileDto
    {
        List<IFormFile> files = new List<IFormFile>();
    }
}
