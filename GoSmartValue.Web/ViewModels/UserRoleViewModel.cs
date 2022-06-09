using System;

namespace GoSmartValue.Web.ViewModels
{
    public class UserRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
