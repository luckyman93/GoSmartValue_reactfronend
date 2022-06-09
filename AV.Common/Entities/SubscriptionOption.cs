using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class SubscriptionOption
    {
        [Key]
        public int Id { get; set; }
        public PaymentFrequency Frequency { get; set; }
        public decimal Price { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}