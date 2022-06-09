using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Location
    {
        public Location()
        {

        }
        public Location(string locationName, bool verified = false)
        {
            Name = locationName;
            Verified = verified;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Locality> LocalAreas { get; set; }
        public bool Verified { get; set; }
    }
}
