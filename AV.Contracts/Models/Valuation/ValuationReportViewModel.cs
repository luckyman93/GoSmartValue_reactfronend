using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class ValuationReportViewModel
    {
        public string TitleDeedNo { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public DateTime TitleDeedDate { get; set; }
        public decimal PlotSize { get; set; }
        public LandUse LandUse { get; set; }
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
        public InstructionModel Instruction { get; set; }
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
    }
}