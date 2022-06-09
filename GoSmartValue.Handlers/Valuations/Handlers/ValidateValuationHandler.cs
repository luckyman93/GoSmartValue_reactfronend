using System;
using System.Threading.Tasks;
using AV.Common.Entities;
using GoSmartValue.Handlers.Valuations.Handlers;

namespace GoSmartValue.Handlers.Valuations.Handlers
{
    public class ValidateValuationHandler : IHandler
    {
        public async Task<GenerateValuationResult> HandleRequest(ValuationRequest request, GenerateValuationResult result)
        {
            //Validate
            throw new NotImplementedException();
        }
    }

    public class GenerateComparableHandler : IHandler
    {
        public async Task<GenerateValuationResult> HandleRequest(ValuationRequest request, GenerateValuationResult result)
        {
            //Create a comparable 

            //PerformComparable

            //Add Comparables to Valuation result
            return null;
        }
    }

    public class AutoAdjustValuationHandler : IHandler
    {
        public async Task<GenerateValuationResult> HandleRequest(ValuationRequest request, GenerateValuationResult result)
        {
            //PerformAdjustment
            return null;
        }
    }
}

public class GenerateValuationResult
{
    public Valuation Valuation { get; set; }
}