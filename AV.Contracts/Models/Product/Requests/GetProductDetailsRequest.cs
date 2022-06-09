using MediatR;

namespace AV.Contracts.Models.Product.Requests
{
    public class GetProductDetailsRequest : IRequest<ProductModel>
    {
        public string Name { get; set; }
    }
}
