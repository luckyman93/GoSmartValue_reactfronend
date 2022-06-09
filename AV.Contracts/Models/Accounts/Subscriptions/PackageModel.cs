using System.Collections.Generic;
using AV.Contracts.Enums;
using AV.Contracts.Models.Product;

namespace AV.Contracts.Models.Accounts.Subscriptions
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        public decimal Price { get; set; }
        public int MaximumSubAccounts { get; set; }
        public int? InstantReportLimit { get; set; }
        public int? DetailedReportLimit { get; set; }
        public double DiscountPerReferral { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<PackageFeatureModel> Features { get; set; }
    }
}