using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market.Requests.Deeds;
using AV.Contracts.Models.Valuation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AV.Handlers.Valuation.Deeds
{
    public class GetDeedsTransactionsRequestHandler : IRequestHandler<GetDeedsTransactionsRequest, IEnumerable<ComparableViewModel>>
    {
        private readonly IRepository<Comparable> _comparablesRepository;
        private readonly IMapper _mapper;

        public GetDeedsTransactionsRequestHandler(
            IRepository<Comparable> comparablesRepository,
            IMapper mapper
        )
        {
            _comparablesRepository = comparablesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComparableViewModel>> Handle(GetDeedsTransactionsRequest request, CancellationToken cancellationToken)
        {
            var comparables = await _comparablesRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.BandClass)
                .Where(c => c.ValuationSource == ValuationSource.DeedsOffice && !c.IsDeleted)
                .OrderByDescending(c => c.AddedOn)
                .Take(request.NumberOfRecords)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ComparableViewModel>>(comparables);
        }
    }
}
