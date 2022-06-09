using System.Collections.Generic;

namespace GoSmartValue.Web.Models
{
    public class DistrictViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int? Population { get; set; }
        //in kilometers squares
        public double? Area { get; set; }
        //per kilometers squared
        public double? Density { get; set; }
        public ICollection<LocationViewModel> Regions { get; set; }
        public bool Verified { get; set; }
    }
}