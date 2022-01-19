using System.Collections.Generic;
using System.Linq;

namespace Web.Utils.ContentType
{
    public static class ContentTypeHelper
    {
        public static string[] Images { get; } = { "image/jpg", "image/jpeg", "image/png", "image/heic", "image/svg+xml" };

        public static Dictionary<string, string> ContentTypes { get; } = new Dictionary<string, string>()
        {
            { "pdf", "application/pdf" },
            { "doc", "application/msword" },
            { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }, //Microsoft Word (OpenXML)
            { "jpg", "image/jpg" },
            { "jpeg", "image/jpeg" },
            { "png", "image/png" },
            { "svg", "image/svg+xml"},
            {"heic", "image/heic" }, // Photo extension in iOS and MacOS
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

        public static string GetContentTypeFromExtension(string extension)
        {
            return ContentTypes.FirstOrDefault(ct => ct.Key == extension).Value;
        }
    }
}
