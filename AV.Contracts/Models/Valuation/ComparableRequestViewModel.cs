using AV.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Valuation
{
    public class ComparableRequestViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int? LocalityId { get; set; }
        public string LocalityName { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public LandUse LandUse { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? Size { get; set; }
        public int? PurchasePrice { get; set; }
        public string Source { get; set; }
        public string Host { get; set; }
        public string RequestUri { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? PlotId { get; set; }
        public int? StreetId { get; set; }
        public IEnumerable<ComparableRequestViewModel> Comparables { get; set; }
        public int PlotSize { get; set; }


        //additional information
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }
        public int BathRooms { get; set; }

        //property features
        public bool SwimmingPool { get; set; }
        public bool FirePlace { get; set; }
        public bool BoundaryWall { get; set; }
        public bool ElectricFence { get; set; }
        public bool MotorizedGate { get; set; }
        public bool OutdoorEntertainmentArea { get; set; }
        public bool Paved { get; set; }
        public string OtherSpecialFeatures { get; set; }
    }
}