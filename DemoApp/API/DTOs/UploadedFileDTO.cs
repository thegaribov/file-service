using API.Constants.Common;
using API.Constants.File;
using API.Helpers.ContentType;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace API.DTOs
{
    public class UploadedFileDTO
    {
        public IFormFile File { get; set; }
    }

    public class UploadedFileDTOValidator : AbstractValidator<UploadedFileDTO>
    {
        private readonly double _allowedMaxUploadSize = 2 * StorageUnits.Megabyte;
        private readonly string[] _allowedContentTypes = { MimeType.Image.Jpeg, MimeType.Image.Jpg, MimeType.Image.Png};

        public UploadedFileDTOValidator()
        {
            IntegrateRules();
        }

        private void IntegrateRules()
        {
            #region UploadedFile

            RuleFor(model => model.File)
                .Cascade(CascadeMode.Stop)

                .NotEmpty()
                .WithMessage("Can't be empty")

                .NotNull()
                .WithMessage("Can't be empty")

                .Must(uploadedFile => uploadedFile.Length <= _allowedMaxUploadSize)
                .WithMessage($"Max allowed upload size : {_allowedMaxUploadSize / StorageUnits.Megabyte} Mb")

                .Must(uploadedFile => _allowedContentTypes.Contains(uploadedFile.ContentType))
                .WithMessage($"This extension is not allowed, allowed extensions are {ContentTypeHelper.GetExtensionsFromRelatedContentTypes(_allowedContentTypes)}");

            #endregion
        }
    }
}
