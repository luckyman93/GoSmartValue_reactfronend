using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts.Subscriptions;
using MediatR;
using System.Collections.Generic;

namespace AV.Contracts.Models.Product.Commands
{
    public class CreateProductCommand : IRequest<ProductModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public string ServiceType { get; set; }
        public string SampleUrl { get; set; }
        public IEnumerable<ProductFeatureModel> Features { get; set; }
    }
}
