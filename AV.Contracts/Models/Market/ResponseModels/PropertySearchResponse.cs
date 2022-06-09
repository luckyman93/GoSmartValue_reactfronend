using AV.Contracts.Models.Valuation;
using System.Collections.Generic;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class PropertySearchResponse
    {
        public PropertySearchResponse()
        {
            PropertyList = new List<ComparableViewModel>();
        }

        public string LocationName { get; set; }
        public string PlotNo { get; set; }
        public string Size { get; set; }
        public IEnumerable<ComparableViewModel> PropertyList { get; set; }


    }

}
