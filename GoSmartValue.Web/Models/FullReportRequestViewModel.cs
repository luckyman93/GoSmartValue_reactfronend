using System;

namespace GoSmartValue.Web.Models
{
    public class FullReportRequestViewModel
    {
        public Guid Reference { get; set; }
        public Guid PropertyDetailsId { get; set; }
        public double EstimatedValue { get; set; }
    }
}