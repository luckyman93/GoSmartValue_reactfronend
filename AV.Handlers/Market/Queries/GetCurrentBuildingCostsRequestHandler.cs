using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Models.Market;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Market.Queries
{
    public class GetCurrentBuildingCostsRequestHandler : IRequestHandler<GetCurrentBuildingCostsRequest, ImportBuildingCostsResponse>
    {
        public IMarketInformationRepository MarketInformationRepository { get; }

        public GetCurrentBuildingCostsRequestHandler(IMarketInformationRepository marketInformationRepository)
        {
            MarketInformationRepository = marketInformationRepository;
        }

        public async Task<ImportBuildingCostsResponse> Handle(GetCurrentBuildingCostsRequest request, CancellationToken cancellationToken)
        {
            var buildingCosts = await MarketInformationRepository.GetBuildingCosts(cancellationToken);
            if (buildingCosts != null)
                return new ImportBuildingCostsResponse
                {
                    Data = MapBuildingCosts(buildingCosts),
                };
            return new ImportBuildingCostsResponse();
        }

        private IEnumerable<BuildingCostsModel> MapBuildingCosts(IEnumerable<BuildingCost> buildingCosts)
        {
            var BuildingCostsModels = new List<BuildingCostsModel>();
            foreach (var buildingCost in buildingCosts)
            {
                var BuildingCostsModel = new BuildingCostsModel();
                BuildingCostsModel.Id = buildingCost.Id;
                BuildingCostsModel.PropertyType = buildingCost.PropertyType;
                BuildingCostsModel.Rate = buildingCost.Rate;
                BuildingCostsModel.StandardSize = buildingCost.StandardSize;
                BuildingCostsModel.Metric = buildingCost.Metric;
                BuildingCostsModels.Add(BuildingCostsModel);
            }

            return BuildingCostsModels;
        }
    }
}
