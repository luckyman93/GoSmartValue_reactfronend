using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Valuation
{
    public class ValuationResultViewModel
    {
        public Guid Id { get; set; }
        public Guid? ValuationId { get; set; }
        public ValuationModel Valuation { get; set; }
        public Guid ValuerId { get; set; }
        public virtual Users.UserModel Valuer { get; set; }
        public ICollection<ComparableViewModel> Comparables { get; set; }
        public InstructionModel Instruction { get; set; }
        public Guid ComparableId { get; set; }
        public virtual ComparableViewModel Comparable { get; set; }
        public Guid InstructionId { get; set; }
        public bool Accepted { get; set; }
        
    }
}