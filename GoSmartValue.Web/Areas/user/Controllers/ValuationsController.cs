using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Services;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.user.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("user")]
    [Route("[controller]/")]
    public class ValuationsController : SecureController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IDocumentStoreService _documentStoreService;
        private readonly IValuationsService _valuationsService;
        private readonly IInstructionService _instructionService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ValuationsController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IDocumentStoreService documentStoreService,
            IValuationsService valuationsService,
            IInstructionService instructionService,
            IUserManagerService userService,
            IMediator mediator,
            IMapper mapper)
            : base(userManager, roleManager, userService, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _documentStoreService = documentStoreService;
            _valuationsService = valuationsService;
            _instructionService = instructionService;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetValuation")]
        [AllowAnonymous]
        public IActionResult GetValuation()
        {
            return View("ValuationRequest");
        }

        [HttpPost("GetValuation")]
        [AllowAnonymous]
        public async Task<IActionResult> GetValuation(ComparableRequestViewModel comparableRequestRequest)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Missing data", "Please ensure all fields are filled in correctly.");
                return View("ValuationRequest", comparableRequestRequest);
            }

            AddHttpRequestDate(comparableRequestRequest);
            var result = (await _valuationsService.CalculateValuation(comparableRequestRequest)).Item1;
            return RedirectToAction("ValuationResult", result);
        }

        [HttpGet]
        [Route("request")]
        public IActionResult ValuationResult(ComparableResultViewModel comparableResult)
        {
            ResetReturnUrl();
            return View(comparableResult);
        }

        [HttpGet]
        [Route("getreport")]
        public async Task<IActionResult> FullReportRequestGet(Guid id)
        {
            var comparableResult = await _valuationsService.GetComparableResult(id);
            comparableResult.ComparableResultId = id;
            return View(comparableResult);
        }

        [HttpGet]
        [Route("pay")]
        public async Task<IActionResult> InstantReportPayment(Guid id)
        {
            var user = CurrentUser;
            var request = await CreateMakePaymentRequest(user, id);
            return RedirectToAction("Payment", "Payment", request);
        }

        [HttpGet]
        [Route("getdetailedreport")]
        public async Task<IActionResult> IssueInstruction(Guid? id = null)
        {
            var instruction = new InstructionModel();
            if (id.HasValue)
            {
                instruction = ConvertToInstruction(await _valuationsService.GetComparableResult(id.Value));
            }
            return View(instruction);
        }

        [HttpPost]
        [Route("getdetailedreport")]
        public async Task<IActionResult> IssueInstruction(InstructionModel instruction, ICollection<IFormFile> UploadDocument = null)
        {
            if (ModelState.IsValid)
            {

                instruction.SupportingDocuments = _documentStoreService.ConvertToInstructionDocument<InstructionDocumentModel>(UploadDocument).ToList();
                instruction.IssuerId = CurrentUser.Id;
                await _instructionService.CreateInstruction(instruction);

                return RedirectToAction("InstructionSent", "Standard");
            }

            return View("IssueInstruction", instruction);
        }

        private async Task<ICollection<DocumentModel>> ExtractDocument(ICollection<IFormFile> documents)
        {
            return await _documentStoreService.SaveFiles(documents);
        }

        [HttpPost]
        [Route("request")]
        public async Task<IActionResult> FullReportRequestPost(ComparableResultViewModel comparableResult)
        {
            if (!ModelState.IsValid)
            {
                return View("ValuationResult", comparableResult);
            }

            if (_signInManager.IsSignedIn(User))
            {
                comparableResult.RequesterUserId = CurrentUser.Id;
            }

            _valuationsService.RequestFullReport(comparableResult);

            return Redirect(await ResolveUserRedirectUrl());
            // return RedirectToAction("Paypal","Home", new {product = comparableResult.ReportType});
        }

        private InstructionModel ConvertToInstruction(ComparableResultViewModel comparableResult)
        {
            return new InstructionModel
            {
                IssuerId = CurrentUser.Id,
                Issuer = _mapper.Map<AV.Contracts.Models.Users.UserModel>(CurrentUser),
                LocationId = comparableResult.Comparable.LocationId,
                Location = comparableResult.Comparable.Location,
                LocalityId = comparableResult.Comparable.LocalityId,
                Locality = comparableResult.Comparable.Locality,
                PlotNumber = comparableResult.Comparable.PlotNo,
            };
        }

        private void AddHttpRequestDate(ComparableRequestViewModel comparableRequestRequest)
        {
            comparableRequestRequest.RequestDate = DateTime.UtcNow;
            comparableRequestRequest.RequestUri = Request.GetDisplayUrl();
            comparableRequestRequest.Host = Request.Host.Host;
        }
        private async Task<MakePaymentRequest> CreateMakePaymentRequest(User user, Guid id)
        {
            var product = await GetProductPrice(PaymentType.InstantReport.ToString());
            return new MakePaymentRequest
            {
                InitiatedByUserId = user.Id,

                Reference = id.ToString(),
                Type = PaymentType.InstantReport,
                Amount = (int)product.Price,
                ServiceName = product.Description,
                ServiceType = product.ServiceType
            };
        }

        private async Task<ProductModel> GetProductPrice(string productName)
        {
            return await _mediator.Send(new GetProductDetailsRequest
            {
                Name = productName,
            });
        }
    }
}