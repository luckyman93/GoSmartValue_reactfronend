using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Reports
{
    public class GetLocationsTransactionsRequestHandler : IRequestHandler<GetLocationsTransactionsRequest, LocationsTransactionsResponse>
    {
        private readonly ILocationUnitOfWork _locationUoW;
        private readonly IComparableRepository _comparableRepository;

        public GetLocationsTransactionsRequestHandler(
            ILocationUnitOfWork locationUoW,
            IComparableRepository comparableRepository)
        {
            _locationUoW = locationUoW;
            _comparableRepository = comparableRepository;
        }

        public async Task<LocationsTransactionsResponse> Handle(GetLocationsTransactionsRequest request, CancellationToken cancellationToken)
        {
            var result = await _comparableRepository.FetchAll(c =>
              c.DataState == DataState.Verified &&
              c.DateOfSale.HasValue &&
              c.DateOfSale >= request.FromDate &&
              c.DateOfSale <= request.ToDate);

            // group by year and month
            var values = result.GroupBy(c => new { c.LocationId })
                .Select(g =>
                    new KeyValuePair<int, int>(g.Key.LocationId, g.Count()));

            var transactions = new LocationsTransactionsResponse();
            foreach (var pair in values)
            {
                var locationName = SetLocationDetails(pair.Key);
                transactions.Data.Add(locationName, pair.Value);
            }

            return transactions;
        }

        private string SetLocationDetails(int id)
        {
            Location location = null;
            if (id > 0)
            {
                location = _locationUoW.GetLocation(id);
            }
            return location.Name;
        }
    }
}
