using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class Valuation
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? ValuationDate { get; set; }
        public ValuationStatus Status { get; set; }
        public Guid ValuerId { get; set; }
        public User Valuer { get; set; }
        public string TitleDeedNo { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public DateTime TitleDeedDate { get; set; }
        public decimal PlotSize { get; set; }
        public LandUse LandUse { get; set; }
        public Zoning Zoning { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public IEnumerable<ValuationDocument> SitePictures { get; set; }
        public decimal BuiltUpArea { get; set; }
        public string CeilingDetails { get; set; }
        public string FloorDetails { get; set; }
        public string Walls { get; set; }
        public string FittingsAndFixtures { get; set; }
        public string Doors { get; set; }
        public string Roofing { get; set; }
        public string Windows { get; set; }
        public string ExteriorFinishes { get; set; }
        public string OtherDetails { get; set; }
        public string MarketCommentary { get; set; }
        public string ToiletAndBathroom { get; set; }
        public bool SwimmingPool { get; set; }
        public bool FirePlace { get; set; }
        public bool BoundaryWall { get; set; }
        public bool ElectricFence { get; set; }
        public bool MotorizedGate { get; set; }
        public bool OutdoorEntertainmentArea { get; set; }
        public bool Paved { get; set; }
        public string OtherSpecialFeatures { get; set; }
        public int? Rating { get; set; }
        [ForeignKey("InstructionId")]
        public Guid InstructionId { get; set; }
        public Instruction Instruction { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? EstimatedValue { get; set; }
        public decimal? Value { get; set; }
        public Guid? ComparableResultId { get; set; }
        public ComparableResult ComparableResult { get; set; }
        public string AdjustComment { get; set; }
        [Required]
        public decimal ServiceFee { get; set; }

        public class ValuationDocument : Document
        {
            [ForeignKey("Valuation")]
            public Guid ValuationId { get; set; }
            public virtual Valuation Valuation { get; set; }
        }
    
}
}