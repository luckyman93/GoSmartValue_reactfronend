using AutoMapper;
using AV.Contracts.Models.Accounts.Subscriptions;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Persistence.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Subscription
{
    public class GetPackagesRequestHandler : IRequestHandler<GetPackagesRequest, ICollection<AV.Contracts.Models.Accounts.Subscriptions.PackageModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPackageQueries _packageQueries;

        public GetPackagesRequestHandler(IMapper mapper, IPackageQueries packageStores)
        {
            _mapper = mapper;
            _packageQueries = packageStores;
        }

        public async Task<ICollection<PackageModel>> Handle(
            GetPackagesRequest request,
            CancellationToken cancellationToken)
        {
            var packages = await _packageQueries.FetchAll(cancellationToken);

            return _mapper.Map<ICollection<PackageModel>>(packages);
        }
    }
}