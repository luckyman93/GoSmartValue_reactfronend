using AV.Contracts.Enums;
using System.Collections.Generic;

namespace AV.Contracts.Models.Valuation.ResponseModels
{
    public class ImportForInstantValuationResponse
	{
		public ImportStatus Status { get; set; }
		public ImportHeader Header { get; set; }
		public IEnumerable<ComparableRequestViewModel> FailedData { get; set; }
		public IDictionary<int, List<string>> ImportErrors { get; set; }
		public IEnumerable<ComparableViewModel> Comparables { get; set; }
		public IEnumerable<ComparableRequestViewModel> ComparableRequests { get; set; }
        public IEnumerable<ComparableResultViewModel> ComparablesResults { get; set; }
    }
}
