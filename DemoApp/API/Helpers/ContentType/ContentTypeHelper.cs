using API.Constants.Common;
using System.Collections.Generic;
using System.Linq;

namespace API.Helpers.ContentType
{
    public static class ContentTypeHelper
    {
        public static string[] Images { get; } = { MimeType.Image.Jpg, MimeType.Image.Jpeg, MimeType.Image.Png, MimeType.Image.SvgXml, MimeType.Image.Heic };

        public static Dictionary<string, string> ContentTypes { get; } = new Dictionary<string, string>()
        {
            { "pdf", MimeType.Application.Pdf},
            { "doc", "application/msword" },
            { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }, //Microsoft Word (OpenXML)
            { "jpg", MimeType.Image.Jpg },
            { "jpeg", MimeType.Image.Jpeg },
            { "png", MimeType.Image.Png },
            { "svg", MimeType.Image.SvgXml },
            {"heic", MimeType.Image.Heic }, // Photo extension in iOS and MacOS
            { "xls", "application/vnd.ms-excel" },
            { "xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } //Microsoft Excel (OpenXML)
        };

        public static string GetExtensionFromRelatedContentType(string contentType)
        {
            return ContentTypes.FirstOrDefault(ct => ct.Value == contentType).Key;
        }

        public static string GetExtensionsFromRelatedContentTypes(List<string> contentTypes)
        {
            IEnumerable<string> extensions = contentTypes.Select(mt => ContentTypes.FirstOrDefault(ct => ct.Value == mt).Key);

            return string.Join(", ", extensions);
        }

        public static string GetExtensionsFromRelatedContentTypes(string[] contentTypes)
        {
            IEnumerable<string> extensions = contentTypes.Select(mt => ContentTypes.FirstOrDefault(ct => ct.Value == mt).Key);

            return string.Join(", ", extensions);
        }


        public static string GetContentTypeFromExtension(string extension)
        {
            return ContentTypes.FirstOrDefault(ct => ct.Key == extension).Value;
        }
    }
}
