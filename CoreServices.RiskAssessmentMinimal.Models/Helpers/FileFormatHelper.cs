
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public class FileFormatHelper
    {
        // eDRS logic: https://landregistry.github.io/bgtechdoc/images/edrs_attachment.png
        public static readonly List<string> allowedFileTypes = new List<string> { "gif", "jpeg", "jpg", "pdf", "tif", "tiff" };
        public static readonly int EDRSMaxAttachmentFileSize = 41943040; // 40 MB

        public static readonly List<string> OS2AllowedFileTypes = new List<string> { "gif", "jpeg", "jpg", "pdf", "tif", "tiff" };
      
        private static readonly byte[] gif = { 71, 73, 70 };
        private static readonly byte[] jpeg = { 255, 216, 255, 224 };
        private static readonly byte[] pdf = { 37, 80, 68, 70 };
        private static readonly byte[] tiff = { 73, 73, 42, 0 };
        private static readonly byte[] tiff2 = { 77, 77, 42 };

        public static string GetContentFileFormat(Stream stream)
        {
            var extension = "unknown";
            var file = new byte[256];
            stream.Read(file, 0, file.Length);

            //Set attachment file extension
            if (file.Take(3).SequenceEqual(gif))
            {
                extension = "gif";
            }
            else if (file.Take(4).SequenceEqual(jpeg))
            {
                extension = "jpg";
            }
            else if (file.Take(4).SequenceEqual(pdf))
            {
                extension = "pdf";
            }
            else if (file.Take(4).SequenceEqual(tiff))
            {
                extension = "tif";
            }
            else if (file.Take(3).SequenceEqual(tiff2))
            {
                extension = "tif";
            }

            return extension;
        }

        public static string GetExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).TrimStart('.');

            //handle jpeg
            if (String.Equals(extension, "jpeg", StringComparison.OrdinalIgnoreCase))
            {
                extension = "jpg";
            }
            //handle tiff
            if (String.Equals(extension, "tiff", StringComparison.OrdinalIgnoreCase))
            {
                extension = "tif";
            }
            return extension;
        }
    }
}
