using System;
using System.Collections.Generic;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Models;

namespace GoSmartValue.Web.ViewModels
{
    public class OrganisationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PostalAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public int? LocationId { get; set; }
        public virtual LocationViewModel Location { get; set; }
        public int? LocalityId { get; set; }
        public virtual LocalityViewModel Locality { get; set; }
        public int? StreetId { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public Guid PrimaryUserId { get; set; }
        public virtual UserModel PrimaryUser { get; set; }
        public ICollection<PhoneViewModel> PhoneNumbers { get; set; }
        public ICollection<AccountViewModel> UserAccounts { get; set; }
    }
}