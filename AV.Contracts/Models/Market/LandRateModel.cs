using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Market
{
    public class LandRateModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int? LocalityId { get; set; }
        public Locality Locality { get; set; }
        public Metric? Metric { get; set; }
        public decimal? Rate { get; set; }
        public int LowIncome { get; set; }
        public int MiddleIncome { get; set; }
        public int HighIncome { get; set; }
        public Zoning Zoning { get; set; }
        public decimal? AveragePrice { get; set; }
    }
}
