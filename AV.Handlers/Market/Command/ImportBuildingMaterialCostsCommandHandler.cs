using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market;
using AV.Contracts.Models.Market.ResponseModels;
using AV.Handlers.Market.Mappers;
using CsvHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Market.Command
{
    public class ImportBuildingMaterialCostsCommandHandler : IRequestHandler<ImportBuildingMaterialCostsCommand, ImportBuildingMaterialCostsResponse>
    {
        private readonly IMarketInformationRepository _marketInformationRepository;
        private readonly ILogger<ImportBuildingMaterialCostsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ImportBuildingMaterialCostsCommandHandler(
            IMarketInformationRepository marketInformationRepository,
            ILogger<ImportBuildingMaterialCostsCommandHandler> logger,
            IMapper mapper)
        {
            _marketInformationRepository = marketInformationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ImportBuildingMaterialCostsResponse> Handle(ImportBuildingMaterialCostsCommand command, CancellationToken cancellationToken)
        {
            var fileStream = ConvertFormFileToStream(command.CsvFormFile);
            var importHeader = new ImportHeader
            {
                FileName = command.CsvFormFile.FileName,
                FileStream = fileStream,
                Type = command.ImportType,
                ImportedById = command.ImportedBy,
                ActiveFrom = DateTimeOffset.UtcNow,
                ActiveTo = DateTimeOffset.UtcNow,
            };

            var buildingMaterialCosts = ReadCsvFile(fileStream, importHeader);

            // Add Market info record
            var importedBuildingCosts = await _marketInformationRepository.AddBuildingMaterialCosts(buildingMaterialCosts, cancellationToken);
            return CreateImportBuildingMaterialCostsResponse(importedBuildingCosts);
        }

        private ImportBuildingMaterialCostsResponse CreateImportBuildingMaterialCostsResponse(IEnumerable<BuildingMaterialCost> importedBuildingCosts)
        {
            return importedBuildingCosts != null
                ? new ImportBuildingMaterialCostsResponse
                {
                    Header = _mapper.Map<Contracts.Models.ImportHeader>(importedBuildingCosts.FirstOrDefault().ImportHeader),
                    Data = _mapper.Map<IEnumerable<BuildingMaterialCostModel>>(importedBuildingCosts),
                    Status = ImportStatus.Completed
                }
                : default;
        }

        private byte[] ConvertFormFileToStream(IFormFile csvFormFile)
        {
            if (csvFormFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    csvFormFile.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private IEnumerable<BuildingMaterialCost> ReadCsvFile(byte[] fileByteArray, ImportHeader importHeader)
        {
            // Read the file and display it line by line.  
            try
            {
                using var fileStream = new MemoryStream(fileByteArray);
                using TextReader reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                
                csv.Context.RegisterClassMap<BuildingMaterialCostMap>();

                var buildingMaterialCost = new List<BuildingMaterialCost>();
                while (csv.Read())
                {
                    var record = csv.GetRecord<BuildingMaterialsCostDto>();
                    buildingMaterialCost.Add(record.AsBuildingCost());
                }
                foreach (var material in buildingMaterialCost)
                {
                    material.ImportHeader = importHeader;
                }
                return buildingMaterialCost;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error on importing Building Material Costs..", exception);
                _logger.LogError($"Error on importing Building Material Costs..", exception.Data.Values);
                throw new GoSmartValueException("Error on importing Building Material Costs..", exception);
            }
        }

        public class BuildingMaterialsCostDto
        {
            public string Material { get; set; }
            public string Item { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }

            public BuildingMaterialCost AsBuildingCost()
            {
                return new BuildingMaterialCost
                {
                    Material = Enum.Parse<Material>(Material),
                    Description = string.IsNullOrEmpty(this.Description) ? "" : this.Description,
                    Item = Item,
                    Price = (decimal)ParseDecimal(Price),
                };
            }
        }

        private static int? ParseInt(string value)
        {
            var success = int.TryParse(value, out var number);
            if (success)
            {
                return number;
            }
            return null;
        }

        private static decimal? ParseDecimal(string value)
        {
            decimal output;
            var success = decimal.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out output);
            if (success)
            {
                return output;
            }
            return null;
        }
    }

}
