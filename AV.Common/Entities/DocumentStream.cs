using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class DocumentStream
    {
        public DocumentStream()
        {

        }

        public DocumentStream(byte[] fileStreamToStore, string mimeType)
        {
            FileStream = fileStreamToStore;
            MimeType = mimeType;
        }

        [Key]
        public Guid Id { get; private set; }
        public string MimeType { get; private set; }
        public byte[] FileStream { get; private set; }

        public double GetSizeInMb()
        {
            return (FileStream.Length / 1024f) / 1024f;
        }
    }
}