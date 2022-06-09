using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class ValuationReportRequest
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset RequestedOn { get; set; }
        public Guid? RequesterId { get; set; }
        public ComparableResult ComparableResult { get; set; }
        public virtual Guid ComparableRequestId { get; set; }
        public Guid ComparableResultId { get; set; }
        public RequestStatus Status { get; set; }
    }
}