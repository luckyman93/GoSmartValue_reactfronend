using AV.Contracts.Enums;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Contracts.Models.Market.Requests
{
    public class PropertySearchRequest : IRequest<PropertySearchResponse>
	{
		public string LocationName { get; set; }
		public string PlotNo { get; set; }
		public string Size { get; set; }
        public PropertySearchType PropertySearchType { get; set; }
    }
}
