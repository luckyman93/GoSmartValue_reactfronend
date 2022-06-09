using System;
using AV.Contracts.Models.Reports.Responses;
using MediatR;

namespace AV.Contracts.Models.Reports.Requests
{
    public class GetValueTrendReportRequest: IRequest<ValueTrendReportResponse>
    {
        public DateTime FromDate { get; set; } = DateTime.Now.AddMonths(-6);
        public DateTime ToDate { get; set; } = DateTime.Now;
        public int LocationId { get; set; }
        public string Location { get; set; }
    }
}
