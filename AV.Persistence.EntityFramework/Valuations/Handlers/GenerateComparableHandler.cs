using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Valuations.Handlers
{
    public class GenerateComparableHandler : Handler
    {
        private readonly IdentityDbContext<Common.Entities.User, Role, Guid> _dbContext;
        private readonly IInstructionRepository _instructionRepository;

        public GenerateComparableHandler(IdentityDbContext<Common.Entities.User, Role, Guid> dbContext, IInstructionRepository instructionRepository)
        {
            _dbContext = dbContext;
            _instructionRepository = instructionRepository;
        }

        public override async Task<ProcessValuationDto> HandleRequest(ValuationRequest request, ProcessValuationDto processValuationDto)
        {
            processValuationDto.Valuation = processValuationDto.Valuation ?? request.Valuation;

            if (processValuationDto.Valuation?.Instruction == null && processValuationDto.Valuation.InstructionId == default)
            {
                processValuationDto.AddMessage($"No instruction found with valuation.");
                return processValuationDto;
            }

            if (processValuationDto.Valuation.Instruction == null)
            {
                processValuationDto.Instruction = _instructionRepository.Get(processValuationDto.Valuation.InstructionId);
            }
            processValuationDto.Comparable = GenerateComparable(processValuationDto);

            if (successor != null)
            {
                await successor.HandleRequest(request, processValuationDto);
            }
            return processValuationDto;
        }

        private Comparable GenerateComparable(ProcessValuationDto processValuationDto)
        {
            var comparable = new Comparable
            {
                AddedBy = processValuationDto.Valuation.ValuerId,
                AddedOn = DateTimeOffset.Now,
                BandClass = GetComparableBandClass(processValuationDto.Valuation.PlotSize),
                DataState = DataState.Raw,
                Features = GetSelectedFeatures(processValuationDto.Valuation),
                LandUse = processValuationDto.Valuation.LandUse,
                Latitude = processValuationDto.Valuation.Latitude,
                Longitude = processValuationDto.Valuation.Longitude,
                LocalityId = processValuationDto.Instruction.LocalityId,
                LocationId = processValuationDto.Instruction.LocationId,
                PlotNo = processValuationDto.Instruction.PlotNumber,
                Metric = Metric.SquareMetres,
                TitleDeedNo = processValuationDto.Valuation.TitleDeedNo
            };

            comparable.DataState = DataState.Raw;
            comparable.PlotSize = ConvertToMetreSquared(processValuationDto.Valuation.PlotSize, Metric.SquareMetres);
            comparable.BandClass = GetComparableBandClass(processValuationDto.Valuation.PlotSize);

            return comparable;
        }

        private decimal ConvertToMetreSquared(decimal? plotSize, Metric metric)
        {
            if (!plotSize.HasValue) return 0;
            return plotSize.Value * Constants.GetMetricMultiplierToMeterSquared(metric);
        }

        private ComparableBandSize GetComparableBandClass(decimal plotSize)
        {
            return _dbContext.Set<ComparableBandSize>()
                       .OrderBy(c => c.LowerBandLimit)
                       .FirstOrDefault(cb =>
                           //size in band
                           (plotSize >= cb.LowerBandLimit && plotSize < cb.UpperBandLimit)
                       ) ??
                   _dbContext.Set<ComparableBandSize>()
                       .OrderBy(c => c.LowerBandLimit)
                       .First();
        }

        private ICollection<PropertyFeature> GetSelectedFeatures(Valuation valuation)
        {
            var features = new List<PropertyFeature>();
            if (valuation.BoundaryWall)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.BoundaryWall });
            }
            if (valuation.ElectricFence)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.ElectricFence });
            }
            if (valuation.FirePlace)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.FirePlace });
            }
            if (valuation.Paved)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.Paved });
            }
            if (valuation.SwimmingPool)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.SwimmingPool });
            }
            if (valuation.MotorizedGate)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.MotorizedGate });
            }
            if (valuation.OutdoorEntertainmentArea)
            {
                features.Add(new PropertyFeature { FeatureType = FeatureType.OutdoorEntertainment });
            }

            return features;
        }
    }
}