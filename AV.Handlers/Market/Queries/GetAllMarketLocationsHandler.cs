using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Handlers.Market.Queries
{
    public class GetAllMarketLocationsHandler : IRequestHandler<GetAllMarketLocationsRequest, IEnumerable<MarketInformation>>
    {
        private readonly IMapper _mapper;
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetAllMarketLocationsHandler(
            IMarketInformationRepository marketInformationRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<IEnumerable<MarketInformation>> Handle(GetAllMarketLocationsRequest request, CancellationToken cancellationToken)
        {
            var marketInformatics = await MarketInformationRepository.GetAllMarketInformationAsync(cancellationToken);

            return new List<MarketInformation>(_mapper.Map<IEnumerable<MarketInformation>>(marketInformatics));
        }
    }
}
