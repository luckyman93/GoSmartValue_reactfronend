using AV.Contracts.Enums;
using System.Collections.Generic;

namespace AV.Contracts.Models.Valuation
{
    public class PropertyInformation
    {
        public Metric Metric { get; set; }
        public decimal PlotSize { get; set; }
        public string PlotNo { get; set; }
        public LandUse LandUse { get; set; }
        public PropertyType PropertyType { get; set; }
        public virtual Location Location { get; set; }
        public virtual Locality Locality { get; set; }
        public ICollection<PropertyFeatureModel> Features { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }
        public int BathRooms { get; set; }
        public string ReportType { get; set; }
    }
}