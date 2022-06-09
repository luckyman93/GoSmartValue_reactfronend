using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Models
{
    public class UserTokenViewModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public bool Redirect { get; set; }
        public string PortalUrl { get; set; }
    }
}
