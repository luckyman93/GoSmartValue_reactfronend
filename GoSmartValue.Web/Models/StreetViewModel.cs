using System.Collections.Generic;

namespace GoSmartValue.Web.Models
{
    public class StreetViewModel
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int LocalityId { get; set; }
        public bool Verified { get; set; }
        public virtual LocalityViewModel Locality { get; set; }
        public virtual ICollection<PlotViewModel> Plots { get; set; }
    }
}