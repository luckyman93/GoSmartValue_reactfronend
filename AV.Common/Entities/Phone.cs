using AV.Contracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Phone
    {
        public Guid Id { get; set; }
        public UsageType Type { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}