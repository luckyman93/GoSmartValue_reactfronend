using System.Threading;
using System.Threading.Tasks;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Handlers.Market.Queries
{
    public class GetAveragePlotSizeHandler : IRequestHandler<GetAveragePlotSizeRequest, GetAveragePlotSizeResponse>
    {
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetAveragePlotSizeHandler(IMarketInformationRepository marketInformationRepository)
        {
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<GetAveragePlotSizeResponse> Handle(GetAveragePlotSizeRequest request, CancellationToken cancellationToken)
        {
            var marketInformation = await MarketInformationRepository.GetMarketInformation(request.Zoning, request.LocationId, request.LocalityId, cancellationToken);

            return new GetAveragePlotSizeResponse
            {
                Size = marketInformation.AveragePlotSize,
                Metric = marketInformation.AveragePlotSizeMetric.ToString()
            };
        }
    }
}
