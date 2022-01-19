using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Web.Constants.File;
using Web.Utils.ContentType;

namespace Web.ViewModels
{
    public class UploadFileViewModel
    {
        public IFormFile ImageFile { get; set; }
    }

    public class UploadFileViewModelValidator : AbstractValidator<UploadFileViewModel>
    {
        private readonly double _allowedMaxUploadSize;
        private readonly List<string> _allowedContentTypes;

        public UploadFileViewModelValidator()
        {
            _allowedMaxUploadSize = 2 * StorageUnits.Megabyte;
            _allowedContentTypes = new List<string> { "image/jpeg", "image/jpg", "image/png" };

            IntegrateRules();
        }

        private void IntegrateRules()
        {
            #region UploadedFile

            RuleFor(model => model.ImageFile)
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
