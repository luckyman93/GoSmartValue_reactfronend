using AV.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace AV.Contracts.Models.Market.ResponseModels
{
    public class ImportBuildingMaterialCostsCommand : IRequest<ImportBuildingMaterialCostsResponse>
    {
        public Guid ImportedBy { get; set; }
        public ImportType ImportType { get; set; } = ImportType.BuildingMaterialCosts;
        public IFormFile CsvFormFile { get; set; }
    }
}
