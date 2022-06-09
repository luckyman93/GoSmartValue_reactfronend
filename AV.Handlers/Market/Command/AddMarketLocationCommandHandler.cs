using AutoMapper;
using AV.Common.DTOs;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MarketInformation = AV.Contracts.Models.Market.ResponseModels.MarketInformation;

namespace AV.Handlers.Market.Command
{
    public class AddMarketLocationCommandHandler : IRequestHandler<AddMarketInformationCommand, MarketInformation>
    {
        private readonly IMarketInformationRepository _marketInformationRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IMapper _mapper;

        public AddMarketLocationCommandHandler(
            IMarketInformationRepository marketInformationRepository,
            ILocationsRepository locationsRepository,
            IMapper mapper)
        {
            _marketInformationRepository = marketInformationRepository;
            _locationsRepository = locationsRepository;
            _mapper = mapper;
        }

        public async Task<MarketInformation> Handle(AddMarketInformationCommand command,
            CancellationToken cancellationToken)
        {
            var locationDetail = await _locationsRepository.CreateLocationGroup(
                command.CountryName,
                command.DistrictName,
                command.LocationName,
                command.LocalityName);

            var marketInfo = CreateMarketInformationEntity(command, locationDetail);

            // Add Market info record
            var marketInformation = await _marketInformationRepository.AddAsync(marketInfo, cancellationToken);
            return marketInformation != null
                ? _mapper.Map<MarketInformation>(marketInformation)
                : default;
        }

        private Common.Entities.MarketInformation CreateMarketInformationEntity(AddMarketInformationCommand command,
            LocationDetail locationDetail)
        {
            return new Common.Entities.MarketInformation
            {
                Location = locationDetail.Location,
                LocationId = locationDetail.Location.Id,
                District = locationDetail.District,
                DistrictId = locationDetail.District.Id,
                Locality = locationDetail.Locality,
                LocalityId = locationDetail.Locality?.Id,
                Country = locationDetail.Country,
                CountryId = locationDetail.Country.Id,
                AveragePlotSize = command.AveragePlotSize,
                AveragePlotSizeMetric = command.AveragePlotSizeMetric,
                AveragePrice = command.AveragePrice,
                FromRate = command.FromRate,
                ToRate = command.ToRate,
                Zoning = command.Zoning,
                Metric = Metric.SquareMetres
            };
        }
    }
}