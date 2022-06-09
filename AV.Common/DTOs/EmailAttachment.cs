using System.IO;

namespace AV.Common.DTOs
{
    public class EmailAttachment
    {
         public EmailAttachment(Stream pdfDocumentStream, string fileName, string mediatType)
        {
            AttachmentDocument = pdfDocumentStream;
            FileName = fileName;
            MediaType = mediatType;
        }

        public Stream AttachmentDocument { get; }
        public string FileName { get; }
        public string MediaType { get; }
    }
}
