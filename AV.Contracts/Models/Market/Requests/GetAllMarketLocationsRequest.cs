using System.Collections.Generic;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Contracts.Models.Market.Requests
{
    public class GetAllMarketLocationsRequest : IRequest<IEnumerable<MarketInformation>>
    {
    }
}