using System;

namespace AV.Contracts.Models.Users
{
    public class UserClaimsViewModel
    {
        public Guid Id { get; set; }
        public string ClaimName { get; set; }
        public ApplicationClaims ClaimType { get; set; }
        public string UserCreateDate { get; set; }
    }

    public enum ApplicationClaims
    {

    }
}