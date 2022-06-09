using System.Collections.Generic;
using AV.Contracts.Models.Valuation;
using MediatR;

namespace AV.Contracts.Models.Market.Requests.Deeds
{
    public class GetDeedsTransactionsRequest : IRequest<IEnumerable<ComparableViewModel>>
    {
        public int NumberOfRecords { get; set; }

        public GetDeedsTransactionsRequest(int numberOfRecords = 200)
        {
            NumberOfRecords = numberOfRecords;
        }
    }
}
