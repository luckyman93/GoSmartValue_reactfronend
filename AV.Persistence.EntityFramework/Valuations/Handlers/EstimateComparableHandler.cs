using System;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Valuations.Handlers
{
    public class EstimateComparableHandler : Handler
    {
        private readonly IdentityDbContext<User, Role, Guid> _dbContext;
        private readonly IComparableRepository _comparableRepository;

        public EstimateComparableHandler(
            IdentityDbContext<User, Role, Guid> dbContext,
            IComparableRepository comparableRepository)
        {
            _dbContext = dbContext;
            _comparableRepository = comparableRepository;
        }

        public override async Task<ProcessValuationDto> HandleRequest(ValuationRequest request,
            ProcessValuationDto processValuationDto)
        {
            var comparable = processValuationDto.Comparable;
            if (comparable == null)
            {
                processValuationDto.MessageResult.AddMessage($"No instruction found with valuation.");
                return processValuationDto;
            }

            var comparableResult = _comparableRepository.PerformComparable(comparable);
            
            comparableResult.Result.Valuation = request.Valuation;
            //comparableResult.Result.ValuationId = request.Valuation.Id;
            
            processValuationDto.Comparable = comparable;
            processValuationDto.ComparableResult = comparableResult.Result;
            if (successor != null)
            {
                processValuationDto = await successor.HandleRequest(request, processValuationDto);
            }
            await _dbContext.SaveChangesAsync();
            return processValuationDto;
        }
    }
}