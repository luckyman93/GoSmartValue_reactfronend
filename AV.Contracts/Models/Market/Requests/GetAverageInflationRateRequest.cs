using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Contracts.Models.Market.Requests
{
    public class GetAverageInflationRateRequest : IRequest<MarketInformation>
    {
        public int LocationId { get; set; }
        public int LocalityId { get; set; }
        public int Zoning { get; set; }

    }
}