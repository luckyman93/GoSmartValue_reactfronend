using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Street
    {
        [Key]
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public bool Verified { get; set; } = false;
        public virtual ICollection<Plot> Plots { get; set; }
    }
}