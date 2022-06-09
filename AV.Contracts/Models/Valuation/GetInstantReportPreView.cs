using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class GetInstantReportPreView
    {
        public Guid Id { get; set; }
        public DateTimeOffset AddedOn { get; set; }
        public virtual Location Location { get; set; }
        public string StreetName { get; set; }
        public string PlotNo { get; set; }
        public PaymentStatus paymentStatus { get; set; }
    }
}
