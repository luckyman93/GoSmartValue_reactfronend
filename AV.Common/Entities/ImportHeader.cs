using System;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class ImportHeader : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public ImportType Type { get; set; }
        public Guid ImportedById { get; set; }
        public DateTimeOffset ActiveFrom { get; set; }
        public DateTimeOffset ActiveTo { get; set; }
        public byte[] FileStream { get; set; }
    }
}