using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Reports
{
    public class GetValueTrendReportRequestHandler : IRequestHandler<GetValueTrendReportRequest, ValueTrendReportResponse>
    {
        private readonly IComparableRepository _comparableRepository;

        public GetValueTrendReportRequestHandler(IComparableRepository comparableRepository)
        {
            _comparableRepository = comparableRepository;
        }

        public async Task<ValueTrendReportResponse> Handle(GetValueTrendReportRequest request, CancellationToken cancellationToken)
        {
            var result = await _comparableRepository.FetchAll(c =>
               c.DataState == DataState.Verified &&
               c.LocationId == request.LocationId &&
               c.DateOfSale.HasValue &&
               c.DateOfSale >= request.FromDate &&
               c.DateOfSale <= request.ToDate);

            // group by year and month
            var values = result.GroupBy(c => new { c.DateOfSale.Value.Year, c.DateOfSale.Value.Month })
                .Select(g =>
                    new KeyValuePair<DateTime, int>(new DateTime(g.Key.Year, g.Key.Month, 1), g.Count()));

            var valueTrends = new ValueTrendReportResponse();
            foreach (var pair in values)
            {
                valueTrends.Data.Add(pair.Key, pair.Value);
            }

            return valueTrends;
        }
    }
}
