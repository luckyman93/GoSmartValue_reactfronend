using System.Collections.Generic;

namespace AV.Contracts.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        //in kilometers squares
        public double Area { get; set; }
        //per kilometers squared
        public double Density { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
