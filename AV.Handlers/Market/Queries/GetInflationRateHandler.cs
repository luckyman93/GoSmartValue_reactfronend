using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Handlers.Market.Queries
{
    public class GetInflationRateHandler : IRequestHandler<GetInflationRateRequest, InflationRateResponse>
    {
        private readonly ISystemConfigurationRepository _iSystemConfigurationRepository;

        public GetInflationRateHandler(ISystemConfigurationRepository iSystemConfigurationRepository)
        {
            _iSystemConfigurationRepository = iSystemConfigurationRepository;
        }

        public async Task<InflationRateResponse> Handle(GetInflationRateRequest request, CancellationToken cancellationToken)
        {
            var config = await _iSystemConfigurationRepository.GetInflationRate() ?? new SystemConfiguration();
            return new InflationRateResponse()
            {
                InflationRate = config.Value ?? string.Empty
            };
        }
    }
}
