using System;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AV.Contracts.Models.Market.Commands
{
    public class ImportBuildingCostsCommand : IRequest<ImportBuildingCostsResponse>
    {
        public Guid ImportedBy { get; set; }
        public ImportType ImportType { get; set; }
        public IFormFile CsvFormFile { get; set; }
    }
}
