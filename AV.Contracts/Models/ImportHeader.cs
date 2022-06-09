using System;
using System.Collections.Generic;
using AV.Contracts.Enums;

namespace AV.Contracts.Models
{
    public class ImportHeader
    {
        public int Id { get; set; }
        public Guid ImportedBy { get; set; }
        public string FileName { get; set; }
        public ImportStatus Status { get; set; }
        public ImportType Type { get; set; }
        public DateTimeOffset ActiveFrom { get; set; }
        public DateTimeOffset? ActiveTo { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
    }
}