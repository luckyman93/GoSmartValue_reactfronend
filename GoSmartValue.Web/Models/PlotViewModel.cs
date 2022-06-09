using System;
using System.Collections.Generic;
using AV.Common.Entities;
using AV.Contracts.Enums;

namespace GoSmartValue.Web.Models
{
    public class PlotViewModel
    {
        public Guid PlotId { get; set; }
        public string PlotNo { get; set; }
        public int WardId { get; set; }
        public virtual StreetViewModel Street { get; set; }
        public int PolygonId { get; set; }
        public virtual ShapeViewModel polygon { get; set; }
        public double? Size { get; set; }
        public LandUse LandUse { get; set; }
    }
}