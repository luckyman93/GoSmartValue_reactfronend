using System;
using System.Linq;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Persistence.EntityFramework.Valuations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class ValuationsUnitOfWork : UnitOfWork, IValuationsUnitOfWork
    {
        private readonly IValuationsRepository _valuationsRepository;
        private readonly ValuationBuilder _valuationBuilder;

        public ValuationsUnitOfWork(
            IdentityDbContext<User, Role, Guid> dbContext,
            IValuationsRepository valuationsRepository,
            ValuationBuilder valuationBuilder)
            : base(dbContext)
        {
            _valuationsRepository = valuationsRepository;
            _valuationBuilder = valuationBuilder;
        }

        public ValuationResult ProcessValuation(Valuation valuation)
        {
            var result = _valuationBuilder.Process(valuation).Result;
            
            var valuationResult = new ValuationResult
            {
                Comparable = result.Comparable,
                ComparableId = result.Comparable.Id,
                Valuation = result.Valuation,
                Id = result.Result.Id,
                ValuerId = valuation.ValuerId,
                ValuationId = result.Valuation.Id,
                Valuer = valuation.Valuer,
                Instruction = valuation.Instruction
            };

            // if there are no comparables an estimate is made using Land Rates hence no comparables
            if (result.ComparableResult.Comparables != null)
            {
                valuationResult.Comparables = result.ComparableResult.Comparables?.Select(c => c.Comparable).ToList();
            }
            return valuationResult;
        }

        public void SaveValuation(Valuation valuation)
        {
            _valuationsRepository.Update(valuation);
            Complete();
        }

        public Valuation GetValuationByInstructionId(Guid instructionId)
        {
            return _valuationsRepository
                .Find(v => v.InstructionId == instructionId)
                .Include(v => v.Instruction)
                .ThenInclude(i => i.Issuer)
                .Include(v => v.Valuer)
                
                .Include(v => v.ComparableResult)
                    .ThenInclude(v => v.Comparable)
                .Include(v => v.ComparableResult.Comparables)
                    .ThenInclude(c => c.Comparable)
                    .Include(v => v.SitePictures)
                .Include(a => a.Valuer.Accounts).Where(i => i.Valuer.Active == true)
                .FirstOrDefault();
        }

        public void AcceptValuation(Guid valuationId, decimal value, decimal valuationServiceFee, string valuationAdjustmentReason)
        {
            var valuation = _valuationsRepository.Find(v => v.Id == valuationId)
                .Include(v => v.Instruction)
                .FirstOrDefault();
                //.Include(v => v.Instruction).FirstOrDefault();

            valuation.Value = value;
            valuation.Status = ValuationStatus.Completed;
            valuation.ValuationDate = DateTime.UtcNow;
            valuation.Instruction.Status = InstructionStatus.Completed;
            valuation.ServiceFee = valuationServiceFee;
            valuation.AdjustComment = valuationAdjustmentReason;
            
            UpdateComparable(valuationId, value);
            
            _valuationsRepository.Update(valuation);
            
            Complete();
        }

        private void UpdateComparable(Guid valuationId, decimal value)
        {
            var comparable = _dbContext.Set<ComparableResult>()
                .Where(c => c.ValuationId == valuationId)
                .Select(c => c.Comparable)
                .FirstOrDefault();
            if (comparable != null)
            {
                comparable.SalePrice = value;
                comparable.DataState = DataState.Verified;
            }
        }

    }
}