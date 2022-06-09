using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class MarketInformation : BaseEntity
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
        public Location Location { get; set; }
        [ForeignKey("Locality")]
        public int? LocalityId { get; set; }
        public Locality Locality { get; set; }
        public Metric Metric { get; set; }
        public double? FromRate { get; set; }
        public double? ToRate { get; set; }
        //Calculated from verified comparables on record creation
        public double AveragePlotSize { get; set; }
        public Metric AveragePlotSizeMetric { get; set; }
        public Zoning Zoning { get; set; }
        public double AveragePrice { get; set; }
    }
}
