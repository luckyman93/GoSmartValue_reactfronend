using AV.Contracts.Enums;
using AV.Contracts.Models.Valuation.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace AV.Contracts.Models.Valuation.Commands
{
    public class ImportInstantValuationCommand : IRequest<ImportForInstantValuationResponse>
    {
        public Guid ImportedBy { get; set; }
        public ImportType ImportType { get; set; }
        public IFormFile CsvFormFile { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestUri { get; set; }
        public string Host { get; set; }
    }
}
