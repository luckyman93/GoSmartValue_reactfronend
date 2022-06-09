using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Product
{
    public class GetProductDetailsRequestHandler : IRequestHandler<GetProductDetailsRequest, ProductModel>
    {
        private readonly IMapper _mapper;
        public IProductsRepository ProductsRepository { get; }
        public GetProductDetailsRequestHandler(IMapper mapper, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            ProductsRepository = productsRepository;
        }

        public async Task<ProductModel> Handle(GetProductDetailsRequest request, CancellationToken cancellationToken)
        {
            var product = await ProductsRepository.GetByName(request.Name, cancellationToken);
            return _mapper.Map<ProductModel>(product);
        }
    }
}
