using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Persistence.Queries;
using AV.Persistence.Stores.Subscriptions;
using MediatR;

namespace AV.Handlers.Subscription
{
    public class CreatePackageRequestHandler: IRequestHandler<CreatePackageCommand, AV.Contracts.Models.Accounts.Subscriptions.PackageModel>
    {
        private readonly IMapper _mapper;
        private readonly IPackageStore _packageStore;
        private readonly IQueries<Common.Entities.Product> _productsQueries;

        public CreatePackageRequestHandler(IMapper mapper, IPackageStore packageStore, IQueries<Common.Entities.Product> productsQueries)
        {
            _mapper = mapper;
            _packageStore = packageStore;
            _productsQueries = productsQueries;
        }

        public async Task<AV.Contracts.Models.Accounts.Subscriptions.PackageModel> Handle(CreatePackageCommand command,
            CancellationToken cancellationToken)
        {
            var package=  await _packageStore.Create(CreatePackageEntity(command));

            return _mapper.Map<AV.Contracts.Models.Accounts.Subscriptions.PackageModel>(package);
        }

        private Package CreatePackageEntity(CreatePackageCommand createPackageCommand)
        {
            var products = _productsQueries.EntityDbSet
                .Where(p => createPackageCommand.ProductIds.Contains(p.ProductId)).ToList();

            return new Package
            {
                Description = createPackageCommand.Description,
                Name = createPackageCommand.Name,
                MaximumSubAccounts = createPackageCommand.MaximumSubAccounts,
                PaymentFrequency = createPackageCommand.PaymentFrequency,
                Price = createPackageCommand.Price,
                InstantReportLimit = createPackageCommand.InstantReportLimit,
                DetailedReportLimit = createPackageCommand.DetailedReportLimit,
                DiscountPerReferral = createPackageCommand.DiscountPerReferral,
                Features = _mapper.Map<ICollection<PackageFeature>>(createPackageCommand.Features),
                Products = products,
              
            };
        }
    }
}
