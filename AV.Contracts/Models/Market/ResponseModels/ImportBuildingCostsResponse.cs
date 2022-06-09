using AV.Contracts.Enums;
using System.Collections.Generic;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class ImportBuildingCostsResponse
    {
        public ImportStatus Status { get; set; }
        public ImportHeader Header { get; set; }
        public IEnumerable<BuildingCostsModel> Data { get; set; }
        public IDictionary<int, List<string>> ImportErrors { get; set; }
    }
}
