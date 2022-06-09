
using System;
using AV.Common.Entities;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IValuationsUnitOfWork: IUnitOfWork
    {
        ValuationResult ProcessValuation(Valuation valuation);
        void SaveValuation(Valuation valuation);
        Valuation GetValuationByInstructionId(Guid instructionId);        
        void AcceptValuation(Guid acceptanceRequestValuationId, decimal valuationValue, decimal valuationServiceFee, string valuationAdjustmentReason);
    }
}