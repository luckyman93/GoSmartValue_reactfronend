using AV.Contracts.Enums;
using MediatR;
using MarketInformation = AV.Contracts.Models.Market.ResponseModels.MarketInformation;

namespace AV.Contracts.Models.Market.Requests
{
    public class GetLandRatesRequest : IRequest<MarketInformation>
    {
        public int LocationId { get; set; }
        public int? LocalityId { get; set; }
        public Zoning Zoning { get; set; }
    }
}
