using System;
using AV.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class BasketItemInputData : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BasketItem")]
        public int BasketItemId { get; set; }
        public BasketItem BasketItem { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        [ForeignKey("Locality")]
        public int? LocalityId { get; set; }
        public Locality Locality { get; set; }
        [ForeignKey("Street")]
        public int? StreetId { get; set; }
        public Street Street { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public int PlotSize { get; set; }
        public Metric PlotSizeMetric { get; set; }
        public Zoning Zoning { get; set; }
        public PropertyType DevelopmentPhase { get; set; }
        // public string EstateName { get; set; }
        // public string Value { get; set; }
        // public DateTimeOffset DateFrom { get; set; }
    }
}