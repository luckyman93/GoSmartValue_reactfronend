using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts.Subscriptions;
using System.Collections.Generic;

namespace AV.Contracts.Models.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }
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
