﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;


namespace API.Services.Utility.FileHandler
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        public const string UploadDirectory = "uploads";
        public const string StaticFilesDirectory = "wwwroot";
        private readonly IFileSystem _fileSystem;

        public FileService(ILogger<FileService> logger, IFileSystem fileSystem)
        {
            _logger = logger;
            _fileSystem = fileSystem;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string path)
        {
            try
            {
                string uniqueFilename = GenerateUniqueFileName(file.FileName);
                string uploadPath = GetOrCreateUploadDirectory(path);
                var filePath = GenerateFilePath(uploadPath, uniqueFilename);

                using (var stream = _fileSystem.FileStream.New(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return uniqueFilename;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return string.Empty;
            }
        }
        public async Task<string> UploadFileAsync(IFormFile file, string fileName, string path)
        {
            try
            {
                string uniqueFilename = GenerateUniqueFileName(fileName);
                string uploadPath = GetOrCreateUploadDirectory(path);
                var filePath = GenerateFilePath(uploadPath, uniqueFilename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return uniqueFilename;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return string.Empty;
            }
        }

        public string UploadFile(IFormFile file, string path)
        {
            try
            {
                string uniqueFilename = GenerateUniqueFileName(file.FileName);
                string uploadPath = GetOrCreateUploadDirectory(path);
                var filePath = GenerateFilePath(uploadPath, uniqueFilename);

                using (var stream = _fileSystem.FileStream.New(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return uniqueFilename;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return string.Empty;
            }
        }
        public string UploadFile(IFormFile file, string fileName, string path)
        {
            try
            {
                string uniqueFilename = GenerateUniqueFileName(fileName);
                string uploadPath = GetOrCreateUploadDirectory(path);
                var filePath = GenerateFilePath(uploadPath, uniqueFilename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return uniqueFilename;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return string.Empty;
            }
        }

        public void DeleteFile(string filename, string path)
        {
            try
            {
                string filePath = GetFilePath(filename, path);

                if (_fileSystem.File.Exists(filePath))
                    _fileSystem.File.Delete(filePath);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");
            }
        }

        public string GetFriendlyFileName(string fileName, bool withExtension = true)
        {
            try
            {
                string friendlyFileName = fileName.Split("_", 2)[1];

                return withExtension ? friendlyFileName : _fileSystem.Path.GetFileNameWithoutExtension(friendlyFileName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return string.Empty;
            }

        }

        public double GetFileSize(string fileName, string path, double storageUnit)
        {
            try
            {
                var filePath = GetFilePath(fileName, path);
                using var fileStream = _fileSystem.File.OpenRead(filePath);

                return fileStream.Length / storageUnit;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return 0;
            }
        }

        public double GetFileSize(string fileName, string path, double storageUnit, byte digits)
        {
            try
            {
                var filePath = GetFilePath(fileName, path);
                using var fileStream = _fileSystem.File.OpenRead(filePath);

                return Math.Round(fileStream.Length / storageUnit, digits);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred");

                return 0;
            }
        }


        public string GenerateUniqueFileName(string fileName)
        {
            return Guid.NewGuid() + "_" + fileName;
        }

        public string GetOrCreateUploadDirectory(string path)
        {
            var uploadPath = _fileSystem.Path.Combine(
                            _fileSystem.Directory.GetCurrentDirectory(), StaticFilesDirectory, UploadDirectory, path);

            if (!_fileSystem.Directory.Exists(uploadPath))
            {
                _fileSystem.Directory.CreateDirectory(uploadPath);
            }

            return uploadPath;
        }

        public string GenerateFilePath(string uploadPath, string filename)
        {
            return _fileSystem.Path.Combine(uploadPath, filename); ;
        }

        public string GetFilePath(string fileName, string path)
        {
            return _fileSystem.Path.Combine(
                        _fileSystem.Directory.GetCurrentDirectory(), 
                        StaticFilesDirectory, 
                        UploadDirectory, 
                        path, 
                        fileName);
        }

        public string GetFileUrl(string fileName, string path)
        {
            return $"/{UploadDirectory}/{path}/{fileName}";
        }
    }
}