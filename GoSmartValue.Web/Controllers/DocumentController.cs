using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Services;
using ClosedXML.Excel;
using GoSmartValue.Web.Models.api;
using GoSmartValue.Web.Services;
using GoSmartValue.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocumentController : SecureController
    {
        private readonly IUserManagerService _userAccountService;
        private readonly IValuationsService _valuationsService;
        private readonly IDocumentService _documentService;
        private readonly IDocumentStoreService _documentStore;
        private readonly IMapper _mapper;

        public DocumentController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserManagerService userAccountService,
            IValuationsService valuationsService,
            IDocumentService documentService,
            IDocumentStoreService documentStore,
            IUserManagerService usersService,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
            _userAccountService = userAccountService;
            _valuationsService = valuationsService;
            _documentService = documentService;
            _documentStore = documentStore;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("upload")]
        [AllowAnonymous]
        public IActionResult Upload(UploadFileRequest uploadRequest)
        {
            return Ok(new FileUploadResponse
            {
                DownloadId = Guid.NewGuid(),
                FileName = "generatedFileName",
                FileExtension = "pdf"
            });
        }

        [HttpPost]
        [Route("secure/upload")]
        public IActionResult SecureUpload(UploadFileRequest uploadRequest)
        {
            return Ok(new FileUploadResponse
            {
                DownloadId = Guid.NewGuid(),
                FileName = "generatedFileName",
                FileExtension = "pdf"
            });
        }


        [HttpGet]
        [Route("document/valuation/{id}")]
        [Authorize(Roles = "valuer,corporate,analyst,admin,standard")]
        public async Task<IActionResult> ValuationReport([FromRoute] Guid id)
        {
            var valuationResult = _valuationsService.GetValuationResult(CurrentUser, id);

            if (valuationResult == default)
                return BadRequest("No data found.");
            if (await IsValuer())
            {
                if (valuationResult.ValuerId != CurrentUser.Id)
                {
                    return BadRequest("No data found.");
                }
            }
            if (await IsCorporate())
            {
                if (valuationResult.Instruction.IssuerId != CurrentUser.Id || !_userAccountService.UserOnAccount(CurrentUser, valuationResult.Instruction.IssuerId))
                {
                    return BadRequest("No data found.");
                }
            }

            return View("ValuationReport", valuationResult);
        }

        [HttpGet]
        public async Task<IActionResult> InstantReport(Guid id)
        {
            var result = await _valuationsService.GetComparableResult(id);
            return View("InstantReport", result);
        }

        [HttpGet]
        [Route("document/{documentId}")]
        public IActionResult Download([FromRoute] Guid documentId)
        {
            var document = _documentStore.GetDocument(documentId);
            if (document == null)
                return BadRequest($"No document found. #documentId{documentId}");
            return File(new MemoryStream(document.DocumentStream.FileStream), document.ContentType);
        }

        [HttpGet]
        [Route("document/GetDocumentByAccountId/{accountId}")]
        public IActionResult GetDocumentByAccountId([FromRoute] Guid accountId)
        {
            var document = _documentStore.GetDocumentByAccountId(accountId);
            if (document == null)
                return BadRequest($"No document found for Account. #accountId${accountId}");
            return File(new MemoryStream(document.FirstOrDefault().DocumentStream.FileStream), document.FirstOrDefault().ContentType);
        }

        [HttpGet]
        [Route("document/instruction/{instructionId}")]
        public IActionResult DownloadByInstruction([FromRoute] Guid instructionId)
        {
            var document = _documentStore.GetDocumentByInstructionId(instructionId);
            if (document == null)
                return BadRequest($"No document found for instruction. ${instructionId}");
            return File(
                new MemoryStream(document.FirstOrDefault().DocumentStream.FileStream)
                , document.FirstOrDefault().ContentType);
        }

        [HttpPost]
        public IActionResult ExportToExcel(ICollection<FinalResult> results)
        {
            return results == null ? BadRequest() : GetFile(results);
        }


        public static IEnumerable<FinalResult> ConvertToExcelRows(IEnumerable<ComparableResultViewModel> results)
        {
            var finalResults = new List<FinalResult>();

            foreach (var result in results)
            {
                if (result?.Comparables?.Count == 0)
                {
                    result.Comparables = null;
                }
                finalResults.Add(new FinalResult
                {
                    Location = result.ComparableRequest.LocationName,
                    Locality = result.ComparableRequest.LocalityName,
                    PlotSize = result.ComparableRequest.Size.ToString(),
                    PlotNo = result.ComparableRequest.PlotNo,
                    Development = result.ComparableRequest.PropertyType.ToString(),
                    Estimate = result.EstimatedValue.ToString("0,00.00"),
                    Comp1 = result?.Comparables?[0]?.SalePrice.ToString("0,00.00"),
                    Comp2 = result?.Comparables?[1]?.SalePrice.ToString("0,00.00"),
                    Comp3 = result?.Comparables?[2]?.SalePrice.ToString("0,00.00")

                });
            }

            return finalResults;
        }

        private IActionResult GetFile(IEnumerable<FinalResult> results)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Instant Valuation Results");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Location";
                worksheet.Cell(currentRow, 2).Value = "Locality";
                worksheet.Cell(currentRow, 3).Value = "Plot Number";
                worksheet.Cell(currentRow, 4).Value = "Plot Size";
                worksheet.Cell(currentRow, 5).Value = "Development";
                worksheet.Cell(currentRow, 6).Value = "Estimate";
                worksheet.Cell(currentRow, 7).Value = "Comparable 1";
                worksheet.Cell(currentRow, 8).Value = "Comparable 2";
                worksheet.Cell(currentRow, 9).Value = "Comparable 3";
                foreach (var comp in results)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = comp.Location;
                    worksheet.Cell(currentRow, 2).Value = comp.Locality;
                    worksheet.Cell(currentRow, 3).Value = comp.PlotNo;
                    worksheet.Cell(currentRow, 4).Value = comp.PlotSize;
                    worksheet.Cell(currentRow, 5).Value = comp.Development;
                    worksheet.Cell(currentRow, 6).Value = comp.Estimate;
                    worksheet.Cell(currentRow, 7).Value = comp.Comp1;
                    worksheet.Cell(currentRow, 8).Value = comp.Comp2;
                    worksheet.Cell(currentRow, 9).Value = comp.Comp3;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"valuationresults-{DateTime.Now}.xlsx");
                }
            }
        }
    }
}