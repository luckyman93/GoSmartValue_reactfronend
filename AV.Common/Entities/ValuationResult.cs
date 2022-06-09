using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class ValuationResult
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Valuation")]
        public Guid ValuationId { get; set; }
        public Valuation Valuation { get; set; }
        [ForeignKey("Valuer")]
        public Guid ValuerId { get; set; }
        [InverseProperty("UserId")]
        public virtual User Valuer { get; set; }
        [ForeignKey("Comparable")]
        public Guid ComparableId { get; set; }
        [InverseProperty("ComparableId")]
        public virtual Comparable Comparable { get; set; }
        public ICollection<Comparable> Comparables { get; set; }
        [InverseProperty("InstructionId")]
        public Instruction Instruction { get; set; }
        [ForeignKey("Instruction")]
        public Guid InstructionId { get; set; }
        public bool Accepted { get; set; }
        
    }
}