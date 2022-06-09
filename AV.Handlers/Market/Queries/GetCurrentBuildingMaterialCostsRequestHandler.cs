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
    public class GetCurrentBuildingMaterialCostsRequestHandler : IRequestHandler<GetCurrentBuildingMaterialCostsRequest, ImportBuildingMaterialCostsResponse>
	{
		private readonly IMarketInformationRepository _marketInformationRepository;

		public GetCurrentBuildingMaterialCostsRequestHandler(IMarketInformationRepository marketInformationRepository)
		{
			_marketInformationRepository = marketInformationRepository;
		}

		public async Task<ImportBuildingMaterialCostsResponse> Handle(GetCurrentBuildingMaterialCostsRequest request, CancellationToken cancellationToken)
		{
			var buildingMaterials = await _marketInformationRepository.GetBuildingMaterialCosts(cancellationToken);

			if (buildingMaterials != null)
				return new ImportBuildingMaterialCostsResponse
				{
					Data = MapBuildingMaterialCosts(buildingMaterials),
				};

			return new ImportBuildingMaterialCostsResponse();
		}

		private IEnumerable<BuildingMaterialCostModel> MapBuildingMaterialCosts(IEnumerable<BuildingMaterialCost> buildingMaterials)
		{
			var buildingMaterialModels = new List<BuildingMaterialCostModel>();
			foreach (var item in buildingMaterials)
			{
				var buildingModel = new BuildingMaterialCostModel
				{
					Id = item.Id,
					Material = item.Material,
					Item = item.Item,
					Price = item.Price,
					Description = item.Description
				};
				buildingMaterialModels.Add(buildingModel);
			}
			return buildingMaterialModels;
		}
	}
}
