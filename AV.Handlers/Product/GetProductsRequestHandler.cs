using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Product
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<ProductModel>>
    {
        private readonly IMapper _mapper;
        public IProductsRepository ProductsRepository { get; }
        public GetProductsRequestHandler(IMapper mapper, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            ProductsRepository = productsRepository;
        }

        public async Task<IEnumerable<ProductModel>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await ProductsRepository.GetAllWithFeatures(cancellationToken);
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }
    }
}
