using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Contracts.Models.Accounts.Subscriptions;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Persistence.Stores;
using MediatR;

namespace AV.Handlers.Product
{
    public class CreatePackagesCommandHandler : IRequestHandler<CreatePackagesCommand, IEnumerable<PackageModel>>
    {
        private readonly IMapper _mapper;
        private readonly IStore<Common.Entities.Package> _packageStore;

        public CreatePackagesCommandHandler(IMapper mapper, IStore<Common.Entities.Package> packageStore)
        {
            _mapper = mapper;
            _packageStore = packageStore;
        }

        public async Task<IEnumerable<PackageModel>> Handle(CreatePackagesCommand request, CancellationToken cancellationToken)
        {
            var packages = _mapper.Map<IEnumerable<Common.Entities.Package>>(request.Packages);
            packages = await _packageStore.Create(packages, cancellationToken);
            return _mapper.Map<IEnumerable<PackageModel>>(packages);
        }
    }
}