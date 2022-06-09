using AV.Contracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public abstract class People
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public Salutation? Salutation { get; set; }
        public string IdentityNumber { get; set; }
    }
}