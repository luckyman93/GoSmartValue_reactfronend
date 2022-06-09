using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class InstructionDocument : Document
    {
        [ForeignKey("Instruction")]
        public Guid InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }
    }
}