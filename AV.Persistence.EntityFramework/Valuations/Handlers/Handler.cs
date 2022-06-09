using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Valuations.Handlers
{
    public abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }
        public abstract Task<ProcessValuationDto> HandleRequest(ValuationRequest request, ProcessValuationDto processValuationDto);
    }
}