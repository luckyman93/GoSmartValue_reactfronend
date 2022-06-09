using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Country
    {
        public Country()
        {
            
        }

        public Country(string countryName, bool verified = false)
        {
            Name = countryName;
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
        public ICollection<District> Districts { get; set; }
        public bool Verified { get; set; }
    }
}