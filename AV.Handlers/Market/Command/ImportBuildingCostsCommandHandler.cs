using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market;
using AV.Contracts.Models.Market.Commands;
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
    public class ImportBuildingCostsCommandHandler : IRequestHandler<ImportBuildingCostsCommand, ImportBuildingCostsResponse>
	{
		private readonly IMarketInformationRepository _marketInformationRepository;
		private readonly ILogger<ImportBuildingCostsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ImportBuildingCostsCommandHandler(
			IMarketInformationRepository marketInformationRepository,
			ILogger<ImportBuildingCostsCommandHandler> logger,
            IMapper mapper
       )
		{
			_marketInformationRepository = marketInformationRepository;
			_logger = logger;
            _mapper = mapper;
        }
        public async Task<ImportBuildingCostsResponse> Handle(ImportBuildingCostsCommand command, CancellationToken cancellationToken)
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

            var buildingCosts = ReadCsvFile(fileStream, importHeader);

            // Add Market info record
            var importedBuildingCosts = await _marketInformationRepository.AddBuildingCosts(buildingCosts, cancellationToken);
            return CreateImportBuildingCostsResponse(importedBuildingCosts);
        }


        private ImportBuildingCostsResponse CreateImportBuildingCostsResponse(IEnumerable<BuildingCost> importedBuildingCosts)
        {
            return importedBuildingCosts != null
                ? new ImportBuildingCostsResponse
                {
                    Header = _mapper.Map<Contracts.Models.ImportHeader>(importedBuildingCosts.FirstOrDefault().ImportHeader),
                    Data = _mapper.Map<IEnumerable<BuildingCostsModel>>(importedBuildingCosts),
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

        private IEnumerable<BuildingCost> ReadCsvFile(byte[] fileByteArray, ImportHeader importHeader)
        {
            // Read the file and display it line by line.  
            try
            {
                using var fileStream = new MemoryStream(fileByteArray);
                using TextReader reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                
                csv.Context.RegisterClassMap<BuildingCostMap>();
                     
                var buildingCost = new List<BuildingCost>();
                while (csv.Read())
                {
                    var record = csv.GetRecord<BuildingCostDto>();
                    buildingCost.Add(record.AsBuildingCost());
                }
                return buildingCost;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error on importing Building Costs..", exception);
                _logger.LogError($"Error on importing Building Costs..", exception.Data.Values);
                throw new GoSmartValueException("Error on importing Building Costs..", exception);
            }
        }

        public class BuildingCostDto
        {
            public string PropertyType { get; set; }
            public string Rate { get; set; }
            public string StandardSize { get; set; }
            public string Metric { get; set; }
            

            public BuildingCost AsBuildingCost()
            {
                return new BuildingCost
                {
                    Metric = string.IsNullOrEmpty(this.Metric) ? Contracts.Enums.Metric.SquareMetres : Enum.Parse<Metric>(this.Metric),
                    StandardSize = ParseDecimal(this.StandardSize),
                    Rate = ParseDecimal(this.Rate),
                    PropertyType = this.PropertyType,

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
            var success = decimal.TryParse(value, NumberStyles.Any, new CultureInfo("en-US"), out decimal output);
            if (success)
                {
                    return output;
                }
                return null;
            }
        }
}
