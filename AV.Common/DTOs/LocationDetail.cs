using AV.Common.Entities;

namespace AV.Common.DTOs
{
    public class LocationDetail
    {
        public Country Country { get; set; }
        public District District { get; set; }
        public Location Location { get; set; }
        public Locality Locality { get; set; }
        public Street Street { get; set; }
    }
}
