using System;

namespace AV.Contracts.Models.Valuation
{
    public class DetailedReportValuer
    {
        public Guid? CompanyId { get; set; }
        public string ReacNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}