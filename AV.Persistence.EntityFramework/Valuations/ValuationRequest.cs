using AV.Common.Entities;

namespace AV.Persistence.EntityFramework.Valuations
{
    public class ValuationRequest
    {
        public Valuation Valuation { get; set; }

        public ValuationRequest(Valuation valuation)
        {
            Valuation = valuation;
        }
    }
}