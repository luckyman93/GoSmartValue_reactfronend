using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class PeopleModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public Salutation? Salutation { get; set; }
        public string IdentityNumber { get; set; }
    }
}