using AV.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AV.Contracts.Models.Comparables
{
	public class ComparableRequest
	{
        public string LocalityName { get; set; }
        public string LocationName { get; set; }
        public string PlotNo { get; set; }
        public string PropertyType { get; set; }
        public string Size { get; set; }
        public string StreetName { get; set; }
        public IEnumerable<ComparableRequestViewModel> Comparables { get; set; }

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
        }
    }
}
