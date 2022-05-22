using API.Constants.File;
using API.DTOs;
using API.Services.Utility.FileHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService _fileService;

        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost("~/upload-file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadedFileDTO uploadedFileDTO)
        {
            string uniquefileName = await _fileService.UploadFileAsync(uploadedFileDTO.File, FilePath.Announcement);

            return Ok();
        }


        [HttpDelete("~/delete-file/{filename}/{path}")]
        public IActionResult DeleteFile([FromRoute] string fileName, [FromRoute] string path)
        {
            _fileService.DeleteFile(fileName, path);

            return Ok();
        }

        [HttpGet("~/file-name/with-exension/{fileName}")]
        public string FriendlyFileNameWithExtension(string fileName)
        {
            var friendlyFileName = _fileService.GetFriendlyFileName(fileName, true);

            return friendlyFileName;
        }

        [HttpGet("~/file-name/without-exension/{fileName}")]
        public string FriendlyFileNameWithoutExtension(string fileName)
        {
            var friendlyFileName = _fileService.GetFriendlyFileName(fileName, false);

            return friendlyFileName;
        }

        [HttpGet("~/file-size/{fileName}/{path}")]
        public double GetFileSize([FromRoute] string fileName, [FromRoute] string path)
        {
            var fileSize = _fileService.GetFileSize(fileName, path, StorageUnits.Megabyte, 5);

            return fileSize;
        }
    }
}
