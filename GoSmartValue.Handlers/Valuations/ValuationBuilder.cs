using System.Collections.Generic;
using AV.Common.Entities;
using GoSmartValue.Handlers.Valuations.Handlers;

namespace GoSmartValue.Handlers.Valuations
{
    public class ValuationBuilder
    {
        private ValuationRequest Request { get;  set; }
        private GenerateValuationResult Result { get; set; }
        private List<IHandler> _handlers = new List<IHandler>();
        public IReadOnlyCollection<IHandler> Handlers => _handlers.AsReadOnly();

        public ValuationBuilder()
        {
            RegisterHandlers();
        }

        public GenerateValuationResult Process(Valuation valuation)
        {
            CreateValuationRequest(valuation);
            foreach (var handler in Handlers)
            {
                handler.HandleRequest(Request, Result);
            }
            return Result;
        }

        private void CreateValuationRequest(Valuation valuation)
        {
            Request = new ValuationRequest(valuation);
        }

        private void RegisterHandlers()
        {
            _handlers = new List<IHandler>{
                new ValidateValuationHandler(),
                new GenerateComparableHandler(),
                new AutoAdjustValuationHandler()
            };
        }

    }
}