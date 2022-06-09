using AV.Contracts.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class Package: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PaymentFrequency PaymentFrequency {  get; set; }
        public decimal Price { get; set; }
        public int MaximumSubAccounts { get; set; }
        public int? InstantReportLimit { get; set; }
        public int? DetailedReportLimit { get; set; }
        public double DiscountPerReferral { get; set; }
        public ICollection<Account> Subscriptions { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<PackageFeature> Features { get; set; }
    }
}
