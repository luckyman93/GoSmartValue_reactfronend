using System;
using Microsoft.AspNetCore.Http;

namespace GoSmartValue.Web.Models.api
{
    public class UploadFileRequest
    {
        public IFormFile FormFile { get; set; }
    }

    public class FileUploadResponse
    {
        public Guid DownloadId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }
}
