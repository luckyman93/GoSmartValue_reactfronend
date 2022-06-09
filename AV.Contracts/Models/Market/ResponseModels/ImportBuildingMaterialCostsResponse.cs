using AV.Contracts.Enums;
using System.Collections.Generic;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class ImportBuildingMaterialCostsResponse
	{
		public ImportStatus Status { get; set; }
		public ImportHeader Header { get; set; }
		public IEnumerable<BuildingMaterialCostModel> Data { get; set; }
		public IDictionary<int, List<string>> ImportErrors { get; set; }
	}
}
