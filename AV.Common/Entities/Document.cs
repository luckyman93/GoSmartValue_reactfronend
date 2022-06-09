using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Document
    {
        public Document()
        {

        }

        public Document(string name, string fileFileName, string contentType, DocumentStream documentStream)
        {
            Name = name;
            FileName = fileFileName;
            ContentType = contentType;
            FileSizeMb = (int)documentStream.GetSizeInMb();
            DocumentStream = documentStream;
            LastUpdatedDate = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int FileSizeMb { get; set; }
        public DocumentStream DocumentStream { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
        public string Url { get; set; }
    }
}