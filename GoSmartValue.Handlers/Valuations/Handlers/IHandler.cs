using System.Threading.Tasks;

namespace GoSmartValue.Handlers.Valuations.Handlers
{
    public interface IHandler
    {
        public Task<GenerateValuationResult> HandleRequest(ValuationRequest request, GenerateValuationResult result);
    }
}