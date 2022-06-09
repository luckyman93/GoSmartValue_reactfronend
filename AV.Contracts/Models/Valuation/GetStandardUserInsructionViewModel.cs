using AV.Contracts.Enums;
using AV.Contracts.Models.Users;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class GetStandardUserInsructionViewModel
    {
        public Guid InstructionId { get; set; }
        public string LocationName { get; set; }
        public string PlotNo { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public ValuerViewModel Valuer { get; set; }
        public InstructionStatus Status { get; set; }
    }
}
