using System;

namespace AV.Contracts.Models.Valuation
{
    public class ReportComparablesViewModel
    {
        public DateTimeOffset? DateOfSale { get; set; }
        public string PlotNumber { get; set; }
        public decimal? PlotSize { get; set; }
        public decimal? Transaction { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}