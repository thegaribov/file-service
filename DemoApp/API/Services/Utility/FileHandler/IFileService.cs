﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Utility.FileHandler
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string path);
        Task<string> UploadFileAsync(IFormFile file, string fileName, string path);

        string UploadFile(IFormFile file, string path);
        string UploadFile(IFormFile file, string fileName, string path);

        void DeleteFile(string filename, string path);
        string GetFriendlyFileName(string fileName, bool withExtension = true);
        double GetFileSize(string fileName, string path, double storageUnit);
        double GetFileSize(string fileName, string path, double storageUnit, byte digits);

        string GenerateUniqueFileName(string fileName);
        string GetOrCreateUploadDirectory(string path);
        string GenerateFilePath(string uploadPath, string filename);
        string GetFilePath(string fileName, string path);
        string GetFileUrl(string fileName, string path);
    }
}
