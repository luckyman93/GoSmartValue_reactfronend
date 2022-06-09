using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class SystemConfiguration:BaseEntity
    {

        [Key]
        public string ItemName { get; set; }
        public string Value { get; set; }

        public SystemConfiguration() { }

        public SystemConfiguration(string itemName, string value)
        {
            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = DateTimeOffset.UtcNow;
            ItemName = itemName;
            Value = value;
        }
    }
}
