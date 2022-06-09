using System.Threading.Tasks;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Persistence.EntityFramework.Valuations.Handlers;

namespace AV.Persistence.EntityFramework.Valuations
{
    public class ValuationBuilder
    {
        private readonly ValidateValuationHandler _validateValuationHandler;
        private readonly GenerateComparableHandler _generateComparableHandler;
        private readonly EstimateComparableHandler _estimateHandler;
        private readonly AutoAdjustValuationHandler _autoAdjustValuationHandler;
        private ValuationRequest Request { get; set; }
        private ProcessValuationDto ProcessValuationDto { get; set; }

        public ValuationBuilder(
            ValidateValuationHandler validateValuationHandler,
            GenerateComparableHandler generateComparableHandler,
            EstimateComparableHandler estimateHandler,
            AutoAdjustValuationHandler autoAdjustValuationHandler)
        {
            _validateValuationHandler = validateValuationHandler;
            _generateComparableHandler = generateComparableHandler;
            _estimateHandler = estimateHandler;
            _autoAdjustValuationHandler = autoAdjustValuationHandler;
            RegisterHandlers();

        }

        public async Task<ProcessValuationDto> Process(Valuation valuation)
        {
            ProcessValuationDto = CreateProcessValuationDto(valuation);
            CreateValuationRequest(valuation);

            ProcessValuationDto = await _validateValuationHandler.HandleRequest(Request, ProcessValuationDto);
            valuation.Instruction = ProcessValuationDto.Instruction;

            return ProcessValuationDto;
        }

        private ProcessValuationDto CreateProcessValuationDto(Valuation valuation)
        {
            return new ProcessValuationDto
            {
                MessageResult = new ValidationResult(),
                Valuation = valuation,
                Instruction = valuation.Instruction,
                Result = new ValuationResult()
            };
        }

        private void CreateValuationRequest(Valuation valuation)
        {
            Request = new ValuationRequest(valuation);
        }

        private void RegisterHandlers()
        {
            _validateValuationHandler.SetSuccessor(_generateComparableHandler);
            _generateComparableHandler.SetSuccessor(_estimateHandler);
            _estimateHandler.SetSuccessor(_autoAdjustValuationHandler);
        }
    }
}