using AV.Contracts.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Models
{
    public class GetUserDetailsModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UserActiveSubscriptionViewModel Subscription { get; set; }
        public object PortalUrl { get;set; }
    }
}
