using System.Collections.Generic;

namespace AV.Contracts.Models.Valuation
{
    public class DetailedReportViewModel
    {
        public DetailedReportViewModel()
        {
            valuer = new DetailedReportValuer();
            ValuationReport = new ValuationReportViewModel();
            Comparables = new List<ReportComparablesViewModel>();
        }
        public DetailedReportValuer valuer { get; set; }
        public ValuationReportViewModel ValuationReport { get; set; }
        public IList<ReportComparablesViewModel> Comparables { get; set; }
    }
}
