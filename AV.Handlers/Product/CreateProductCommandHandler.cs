using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Commands;
using AV.Persistence.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace AV.Handlers.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductModel>
    {
        private readonly IMapper _mapper;
        private readonly IStore<Common.Entities.Product> _productStore;

        public CreateProductCommandHandler( IMapper mapper, IStore<Common.Entities.Product> productStore)
        {
            _mapper = mapper;
            _productStore = productStore;
        }

        public async Task<ProductModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Common.Entities.Product>(request);
            product = await _productStore.Create(product, cancellationToken);
            return _mapper.Map<ProductModel>(product);
        }
    }
}
