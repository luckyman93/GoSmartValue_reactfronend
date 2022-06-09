using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;
using AV.Contracts.Models.Users;

namespace AV.Contracts.Models.Valuation
{
    public class ValuationModel
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ValuationDate { get; set; }
        public ValuationStatus Status { get; set; }
        public Guid ValuerId { get; set; }
        public ValuerViewModel Valuer { get; set; }
        public string TitleDeedNo { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public DateTime TitleDeedDate { get; set; }
        public decimal PlotSize { get; set; }
        public LandUse LandUse { get; set; }
        public Zoning Zoning { get; set; }
        public ICollection<AmenityViewModel> Amenities { get; set; }
        public IEnumerable<ValuationDocumentModel> SitePictures { get; set; }
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
        public Guid InstructionId { get; set; }
        public InstructionModel Instruction { get; set; }
        public decimal? EstimatedValue { get; set; }
        public decimal? Value { get; set; }
        public string AdjustmentReason { get; set; }
        [Required]
        public decimal ServiceFee { get; set; }
        public bool IsPaid { get; set; }
    }

    public class AmenityViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}