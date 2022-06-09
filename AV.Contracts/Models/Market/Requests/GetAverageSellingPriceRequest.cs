using AV.Contracts.Enums;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Contracts.Models.Market.Requests
{
    public class GetAverageSellingPriceRequest : IRequest<GetAverageSellingPriceResponse>
    {
        public int LocationId { get; set; }
        public int LocalityId { get; set; }
        public Zoning Zoning { get; set; }
        public int PlotSize { get; set; }
        public Metric Metric { get; set; }
    }
}