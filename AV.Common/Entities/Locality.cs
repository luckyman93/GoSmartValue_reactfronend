using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Locality
    {
        public Locality()
        {

        }
        public Locality(int locationId, string localityName, bool verified = false)
        {
            LocationId = locationId;
            Name = localityName;
            Verified = verified;
        }

        [Key]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public virtual ICollection<Street> Streets { get; set; }
    }
}