using System;
using System.Collections.Generic;

namespace AV.Contracts.Models
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PostalAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public int? StreetId { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public Guid PrimaryUserId { get; set; }
        public virtual UserModel PrimaryUser { get; set; }
        public ICollection<Phone> PhoneNumbers { get; set; }
        public ICollection<Account> UserAccounts { get; set; }
    }
}