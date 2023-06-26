using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.FileDto;
using jts_backend.Models;

namespace jts_backend.Helper
{
    public static class Helper
    {
        public static void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(
            string password,
            byte[] passwordHash,
            byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public static async Task<FileModel> UploadFiles(IFormFile file, string rootPath)
        {
            FileModel uploadedFile = new FileModel();

            if (file.Length > 0)
            {
                var storedFileName = Path.GetRandomFileName();
                var originalFileName = file.FileName;
                var contentType = file.ContentType;

                var filePath = Path.Combine(rootPath, "Uploads", storedFileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                var fileData = new FileModel()
                {
                    file_url = filePath,
                    content_type = contentType,
                    original_file_name = originalFileName,
                    stored_file_name = storedFileName
                };

                uploadedFile = fileData;
            }
            return uploadedFile;
        }
    }
}
