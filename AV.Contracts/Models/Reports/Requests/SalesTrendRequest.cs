using AV.Contracts.Models.Reports.Responses;
using MediatR;
using System;

namespace AV.Contracts.Models.Reports.Requests
{
    public class SalesTrendRequest:IRequest<SalesTrendResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationId { get; set; }
    }
}
