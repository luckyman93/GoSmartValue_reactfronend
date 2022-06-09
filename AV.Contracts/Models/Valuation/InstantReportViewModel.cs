using System;
using System.Collections.Generic;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Valuation
{
    public class InstantReportViewModel
    {
        public InstantReportViewModel()
        {
            Comparables = new List<ReportComparablesViewModel>();
            PropertyInfor = new PropertyInformation();
        }

        public double EstimatedValue { get; set; }
        public DateTimeOffset EstimatedOn { get; set; }
        public IList<ReportComparablesViewModel> Comparables { get; set; }
        public PropertyInformation PropertyInfor { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
