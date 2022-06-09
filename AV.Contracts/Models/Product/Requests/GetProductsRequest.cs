using MediatR;
using System.Collections.Generic;

namespace AV.Contracts.Models.Product.Requests
{
    public class GetProductsRequest : IRequest<IEnumerable<ProductModel>>
    {
    }
}
