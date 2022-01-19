using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Constants.File;
using Web.Models;
using Web.Services.FileService;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileService _fileService;

        public HomeController(ILogger<HomeController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFile(IFormFile imageFile)
        {
            string uniquefileName = await _fileService.UploadFileAsync(imageFile, FilePath.Announcement);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteFile(string fileName, string path)
        {
            _fileService.DeleteFile(fileName, path);

            return RedirectToAction(nameof(Index));
        }

        public string FriendlyFileNameWithExtension(string fileName)
        {
            var friendlyFileName = _fileService.GetFriendlyFileName(fileName, true);

            return friendlyFileName;
        }

        public string FriendlyFileNameWithoutExtension(string fileName)
        {
            var friendlyFileName = _fileService.GetFriendlyFileName(fileName, false);

            return friendlyFileName;
        }

        public IActionResult GetFile(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
           
            return new FileStreamResult(fileStream, "text/plain")
            {
                FileDownloadName = "test.txt"
            };
        }

        public double GetFileSize(string fileName, string path)
        {
            var fileSize = _fileService.GetFileSize(fileName, path, StorageUnits.Megabyte, 5);

            return fileSize;
        }
    }
}
