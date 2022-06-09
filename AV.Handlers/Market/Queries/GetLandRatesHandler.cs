using System.Threading;
using System.Threading.Tasks;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Handlers.Market.Queries
{
    public class GetLandRatesHandler : IRequestHandler<GetLandRatesRequest, MarketInformation>
    {
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetLandRatesHandler(IMarketInformationRepository marketInformationRepository)
        {
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<MarketInformation> Handle(GetLandRatesRequest request, CancellationToken cancellationToken)
        {
            var marketInformation = await MarketInformationRepository.GetMarketInformation(request.Zoning, request.LocationId, request.LocalityId, cancellationToken);
            return marketInformation != null
                ? new MarketInformation
                {
                    Id = marketInformation.Id,
                    CountryId = marketInformation.CountryId,
                    DistrictId = marketInformation.CountryId,
                    LocationId = marketInformation.LocationId,
                    LocalityId = marketInformation.LocalityId,
                    FromRate = marketInformation.FromRate,
                    Metric = marketInformation.Metric,
                    ToRate = marketInformation.ToRate,
                    Zoning = marketInformation.Zoning
                }
                : default;
        }
    }
}
