using System.Collections.Generic;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class ImportLandRatesResponse
    {
        public ImportStatus Status { get; set; }
        public ImportHeader Header { get; set; }
        public IEnumerable<LandRateModel> Data { get; set; }
        public IDictionary<int, List<string>> ImportErrors { get; set; }
    }
}
