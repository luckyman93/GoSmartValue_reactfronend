using System.Collections.Generic;
using System.Threading;
using AV.Contracts.Enums;
using MediatR;

namespace AV.Contracts.Models.Accounts.Subscriptions.Command
{
    public class CreatePackageCommand : IRequest<PackageModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        public decimal Price { get; set; }
        public int MaximumSubAccounts { get; set; }
        public int? InstantReportLimit { get; set; }
        public int? DetailedReportLimit { get; set; }
        public double DiscountPerReferral { get; set; }
        public ICollection<CreatePackageFeatureCommand> Features { get; set; }
        public ICollection<int> ProductIds { get; set; }
    }
}