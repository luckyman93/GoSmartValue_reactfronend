using AV.Common.Entities;

public class ValuationRequest
{
    private Valuation Valuation { get; set; }

    public ValuationRequest(Valuation valuation)
    {
        Valuation = valuation;
    }
}