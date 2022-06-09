using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;

namespace AV.Persistence.EntityFramework.Valuations.Handlers
{
    public class AutoAdjustValuationHandler : Handler
    {
        private readonly IValuationsRepository _valuationsRepository;

        public AutoAdjustValuationHandler(IValuationsRepository valuationsRepository)
        {
            _valuationsRepository = valuationsRepository;
        }

        public override async Task<ProcessValuationDto> HandleRequest(ValuationRequest request, ProcessValuationDto processValuationDto)
        {
            if (processValuationDto.Valuation == null)
            {
                processValuationDto.AddMessage("No valuation record to perform adjustment");
            }
            //PerformAdjustment
            var adjustment = (decimal)(ValueFeatureCount(request.Valuation) * 10) + 100;
            adjustment = adjustment / 100;
            request.Valuation.EstimatedValue =
                processValuationDto.Comparable.SalePrice * adjustment;

            request.Valuation.Value = request.Valuation.EstimatedValue;
            request.Valuation.Status = ValuationStatus.Estimated;

            //Save Adjusted valuation
            _valuationsRepository.Add(request.Valuation);

            processValuationDto.Valuation = request.Valuation;
            if (successor != null)
            {
                processValuationDto = await successor.HandleRequest(request, processValuationDto);
            }
            return processValuationDto;
        }

        private static int ValueFeatureCount(Valuation valuation)
        {
            var count = 0;
            if (valuation.MotorizedGate)
            {
                count++;
            }
            if (valuation.ElectricFence)
            {
                count++;
            }
            if (valuation.OutdoorEntertainmentArea)
            {
                count++;
            }
            if (valuation.SwimmingPool)
            {
                count++;
            }
            if (valuation.FirePlace)
            {
                count++;
            }
            if (valuation.BoundaryWall)
            {
                count++;
            }
            if (valuation.Paved)
            {
                count++;
            }
            return count;
        }
    }
}