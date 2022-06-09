using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Contracts.Models.Valuation;

namespace AV.Contracts.Interface
{
    public interface IUserReportService
    {
        IList<GetInstantReportPreView> GetInstantReports(Guid userId);
        IList<DetailedReportViewModel> GetDetailedReports(Guid userId);
    }
}
