using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;
using Country = AV.Contracts.Models.Country;
using District = AV.Contracts.Models.District;
using Locality = AV.Contracts.Models.Locality;
using Location = AV.Contracts.Models.Location;

namespace AV.Handlers.Market.Queries
{
    public class GetAllCurrentLandRatesRequestHandler : IRequestHandler<GetAllCurrentLandRatesRequest, ImportLandRatesResponse>
    {
        private readonly IMapper _mapper;
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetAllCurrentLandRatesRequestHandler(IMarketInformationRepository marketInformationRepository
        ,IMapper mapper)
        {
            _mapper = mapper;
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<ImportLandRatesResponse> Handle(GetAllCurrentLandRatesRequest request, CancellationToken cancellationToken)
        {
            var landRates = await MarketInformationRepository.GetLandRates(cancellationToken);

            if (landRates != null)
                return new ImportLandRatesResponse
                {
                    Data = MapLandRates(landRates),
                };

            return new ImportLandRatesResponse();
        }

        private IEnumerable<LandRateModel> MapLandRates(IEnumerable<LandRate> landRates)
        {
            var landRateModels = new List<LandRateModel>();
            foreach (var landRate in landRates)
            {
                var landRateModel = new LandRateModel();
                landRateModel.Id = landRate.Id;
                landRateModel.Country = _mapper.Map<Country>(landRate.Country);
                landRateModel.District = _mapper.Map<District>(landRate.District);
                landRateModel.Location = _mapper.Map<Location>(landRate.Location);
                landRateModel.Locality = _mapper.Map<Locality>(landRate.Locality);
                landRateModel.Rate = landRate.Rate;
                landRateModel.Metric = landRate.Metric;
                landRateModel.LowIncome = landRate.LowIncome;
                landRateModel.MiddleIncome = landRate.MiddleIncome;
                landRateModel.HighIncome = landRate.HighIncome;
                landRateModel.Zoning = landRate.Zoning;
                landRateModel.AveragePrice = landRate.AveragePrice;
                landRateModels.Add(landRateModel);
            }

            return landRateModels;
        }
    }
}