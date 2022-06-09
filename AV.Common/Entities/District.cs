using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class District
    {
        public District()
        {
            
        }

        public District(string districtName, bool verified = false)
        {
            Name = districtName;
            Verified = verified;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Population { get; set; }
        //in kilometers squares
        public double? Area { get; set; }
        //per kilometers squared
        public double? Density { get; set; }
        public ICollection<Location> Regions { get; set; }
        public bool Verified { get; set; }
    }
}