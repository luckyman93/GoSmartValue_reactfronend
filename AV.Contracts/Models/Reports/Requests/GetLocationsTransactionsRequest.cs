using AV.Contracts.Models.Reports.Responses;
using MediatR;
using System;

namespace AV.Contracts.Models.Reports.Requests
{
    public class GetLocationsTransactionsRequest : IRequest<LocationsTransactionsResponse>
	{
		public DateTime FromDate { get; set; } //= DateTime.Now.AddMonths(-6);
		public DateTime ToDate { get; set; } //= DateTime.Now;
	}
}
