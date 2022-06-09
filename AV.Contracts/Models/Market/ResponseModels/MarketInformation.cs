using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class MarketInformation
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public string CountryName { get; set; }
        public int? DistrictId { get; set; }
        public District District { get; set; }
        public string DistrictName { get; set; }
        [Required]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public Location Location { get; set; }
        public int? LocalityId { get; set; }
        public Locality Locality { get; set; }
        public string LocalityName { get; set; }
        public Metric Metric { get; set; }
        public double? FromRate { get; set; }
        public double? ToRate { get; set; }
        public Zoning Zoning { get; set; }
        public double AveragePlotSize { get; set; }
        public Metric AveragePlotSizeMetric { get; set; }
        public double AveragePrice { get; set; }
    }
}
