using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class LandRate : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [ForeignKey("District")]
        public int? DistrictId { get; set; }
        public District District { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("Locality")]
        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public Metric Metric { get; set; }
        public decimal? Rate { get; set; }
        //Calculated from verified comparables on record creation
        public int LowIncome { get; set; }
        public int MiddleIncome { get; set; }
        public int HighIncome { get; set; }
        public Zoning Zoning { get; set; }
        public decimal? AveragePrice { get; set; }
        public ImportHeader ImportHeader { get; set; }
    }
}