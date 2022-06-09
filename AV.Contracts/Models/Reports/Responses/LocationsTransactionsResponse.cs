using System.Collections.Generic;

namespace AV.Contracts.Models.Reports.Responses
{
    public class LocationsTransactionsResponse
	{
		public LocationsTransactionsResponse()
		{
			Data = new Dictionary<string, int>();
		}

		public Dictionary<string, int> Data { get; set; }
	}
}
