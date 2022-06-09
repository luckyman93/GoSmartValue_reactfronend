using AV.Contracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class InstructionHistory : BaseEntity
    {
        public InstructionHistory(Guid instructionId, InstructionStatus status, string summary = null, Guid? generatedBy = null)
        {
            InstructionId = instructionId;
            Status = status;
            Summary = summary;
            GeneratedBy = generatedBy;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid InstructionId { get; set; }
        public InstructionStatus Status { get; set; }
        public string Summary { get; set; }
        public Guid? GeneratedBy { get; set; }
        public bool Internal { get; set; }
    }
}