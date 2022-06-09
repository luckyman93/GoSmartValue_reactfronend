using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class InstructionHistoryModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid InstructionId { get; set; }
        public InstructionModel Instruction { get; set; }
        public InstructionStatus Status { get; set; }
        public string Summary { get; set; }
        public Guid? GeneratedBy { get; set; }
        public bool Internal { get; set; }
    }
}