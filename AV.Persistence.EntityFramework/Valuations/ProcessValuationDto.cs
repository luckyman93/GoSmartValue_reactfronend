using AV.Common.DTOs;
using AV.Common.Entities;

namespace AV.Persistence.EntityFramework.Valuations
{
    public class ProcessValuationDto
    {
        public Valuation Valuation { get; set; }
        public ValuationResult Result { get; set; }
        public ValidationResult MessageResult { get; set; }
        public Comparable Comparable { get; set; }
        public Instruction Instruction { get; set; }
        public ComparableResult ComparableResult { get; set; }

        public void AddMessage(string message)
        {
            MessageResult.AddMessage(message);
        }
    }
}