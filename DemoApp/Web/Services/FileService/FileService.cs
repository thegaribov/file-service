﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.FileService
{
    public class FileService : IFileService
    {
        public static string UploadDirectory { get; set; } = "uploads";
        public static string StaticFilesDirectory { get; set; } = "wwwroot";

        public async Task<string> UploadFileAsync(IFormFile file, string path)
        {
            string uniqueFilename = GenerateUniqueFileName(file.FileName);
            string uploadPath = GetOrCreateUploadDirectory(path);
            var filePath = GenerateFilePath(uploadPath, uniqueFilename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFilename;
        }

        public string UploadFile(IFormFile file, string path)
        {
            string uniqueFilename = GenerateUniqueFileName(file.FileName);
            string uploadPath = GetOrCreateUploadDirectory(path);
            var filePath = GenerateFilePath(uploadPath, uniqueFilename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueFilename;
        }

        public string GenerateUniqueFileName(string fileName)
        {
            return Guid.NewGuid() + "_" + fileName;
        }

        public string GetOrCreateUploadDirectory(string path)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), StaticFilesDirectory, UploadDirectory, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            return uploadPath;
        }

        public string GenerateFilePath(string uploadPath, string filename)
        {
            return Path.Combine(uploadPath, filename); ;
        }
    }
}