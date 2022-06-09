using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AV.Contracts.Models.Accounts.Subscriptions;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Persistence.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Persistence.Queries;

namespace AV.Handlers.Product
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, PackageModel>
    {
        private readonly IMapper _mapper;
        private readonly IStore<Common.Entities.Package> _packageStore;
        private readonly IProductsRepository _productsRepository;

        public CreatePackageCommandHandler(IMapper mapper, IStore<Common.Entities.Package> packageStore, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _packageStore = packageStore;
            _productsRepository = productsRepository;
        }

        public async Task<PackageModel> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            var package = ConvertToDomainPackage(request);
            package = await _packageStore.Create(package, cancellationToken);
            return _mapper.Map<PackageModel>(package);
        }

        private Package ConvertToDomainPackage(CreatePackageCommand request)
        {
            return new Package()
            {
                Name = request.Name,
                Description = request.Description,
                PaymentFrequency = request.PaymentFrequency,
                Price = request.Price,
                MaximumSubAccounts = request.MaximumSubAccounts,
                InstantReportLimit = request.InstantReportLimit,
                DetailedReportLimit = request.DetailedReportLimit,
                DiscountPerReferral = request.DiscountPerReferral,
                Features = ConvertToFeatures(request.Features),
                Products = GetProducts(request.ProductIds)
            };
        }

        private ICollection<Common.Entities.Product> GetProducts(ICollection<int> requestProductIds)
        {
            return _productsRepository.Find(p => requestProductIds.Contains(p.ProductId)).ToList();
        }

        private ICollection<PackageFeature> ConvertToFeatures(ICollection<CreatePackageFeatureCommand> requestFeatures)
        {
            return (ICollection<PackageFeature>) requestFeatures.Select(f => new PackageFeature()
            {
                Title = f.Title,
                Description = f.Description,
                Position = f.Position
            });
        }
    }
}