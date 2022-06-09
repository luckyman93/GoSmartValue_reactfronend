using System.Threading;
using System.Threading.Tasks;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Handlers.Market.Queries
{
    public class GetAverageSellingPriceHandler : IRequestHandler<GetAverageSellingPriceRequest, GetAverageSellingPriceResponse>
    {
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetAverageSellingPriceHandler(IMarketInformationRepository marketInformationRepository)
        {
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<GetAverageSellingPriceResponse> Handle(GetAverageSellingPriceRequest request, CancellationToken cancellationToken)
        {
            var marketInformation = await MarketInformationRepository.GetMarketInformation(request.Zoning, request.LocationId, request.LocalityId, cancellationToken);

            return new GetAverageSellingPriceResponse
            {
                Price = marketInformation.AveragePrice,
            };
        }
    }
}
