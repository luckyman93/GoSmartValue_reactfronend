using System;

namespace AV.Contracts.Models.Users
{
    public class ValuerViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string AgencyName { get; set; }
        public Guid? CompanyId { get; set; }
        public string LogoUrl { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
