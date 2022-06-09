using System;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Models;
using Microsoft.AspNetCore.Http;

namespace GoSmartValue.Web.ViewModels
{
    public class RegisterUserViewModel: UserModel
    {
        public Guid ComparableReference { get; set; }
		public string IDNumber { get; set; }
        public IFormFile IDDocument { get; set; }
        public bool BlackAccount { get; set; }
        public bool GreenAccount { get; set; }
        public bool ValuerSingleUse { get; set; }
        public bool ValuerBusiness { get; set; }
        public bool ValuerFastTrack { get; set; }

    }
}