using System.Collections.Generic;

namespace AV.Contracts.Models.Users
{
    public class UserDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserRolesViewModel> Roles { get; set; }
    }
}
