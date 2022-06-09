using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Valuations.Handlers
{
    public class ValidateValuationHandler : Handler
    {
        private ValuationRequest Request { get; set; }
        private ProcessValuationDto ProcessValuationDto { get; set; }

        public override async Task<ProcessValuationDto> HandleRequest(ValuationRequest request, ProcessValuationDto processValuationDto)
        {
            Request = request;
            ProcessValuationDto = processValuationDto;
            if (!IsValid())
            {
                processValuationDto.AddMessage("Request to process valuation is not valid.");
            }
            if (successor != null)
            {
                await successor.HandleRequest(request, processValuationDto);
            }
            return processValuationDto;
        }

        private bool IsValid()
        {
            if (Request.Valuation == null)
            {
                ProcessValuationDto.AddMessage($"Valuation has not been set.");
                return false;
            }

            if (Request.Valuation.InstructionId == default)
            {
                ProcessValuationDto.AddMessage($"No Instruction found with valuation.");
                return false;
            }
            return ProcessValuationDto.MessageResult.IsValid;
        }
    }
}