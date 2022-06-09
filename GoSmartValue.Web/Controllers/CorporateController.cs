using AV.Common.Entities;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using AV.Contracts.Models.Users.Requests;
using System.Threading.Tasks;
using MediatR;
using AV.Contracts.Models.Market.Requests;
using Newtonsoft.Json;
using System.Threading;
using AV.Contracts.Models.Valuation.Commands;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AV.Contracts;
using AV.Contracts.Models;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Models.GraphModels;
using ComparableRequestViewModel = AV.Contracts.Models.Valuation.ComparableRequestViewModel;
using User = AV.Common.Entities.User;
using AV.Contracts.Enums;
using AV.Contracts.Services;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.Corporate)]
    public class CorporateController : SecureController
    {
	    private readonly IDocumentStoreService _documentStoreService;
	    private readonly IInstructionService _instructionService;
        private readonly IMarketInformationService _marketInformationService;
        private readonly IValuationsService _valuationsService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserManagerService _usersService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CorporateController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDocumentStoreService documentStoreService,
            IInstructionService instructionService,
            IMarketInformationService marketInformationService,
            IValuationsService valuationsService,
            IUserManagerService usersService,
            IWebHostEnvironment hostingEnvironment,
            IMediator mediator,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
	        _documentStoreService = documentStoreService;
	        _instructionService = instructionService;
            _marketInformationService = marketInformationService;
            _valuationsService = valuationsService;
            _usersService = usersService;
            _hostingEnvironment = hostingEnvironment;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/instruction/new")]
        public IActionResult Instruct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IssueInstruction(InstructionModel instruction, ICollection<IFormFile> UploadDocument = null)
        {
            if (ModelState.IsValid)
            {
                if (await CanTransact(SubscriptionType.FastTrack))
                {

                    var instructionDocuments = _documentStoreService
                                            .ConvertToInstructionDocument<InstructionDocumentModel>(UploadDocument)?.ToList();
                    instruction.SupportingDocuments = _mapper.Map<ICollection<InstructionDocumentModel>>(instructionDocuments);
                    instruction.IssuerId = CurrentUser.Id;
                    await _instructionService.CreateInstruction(instruction);

                    return RedirectToAction("PendingInstruction");
                }
                else
                {
                    var account = CurrentUser.Accounts.FirstOrDefault();
                    //var request = await _usersService.CreateMakePaymentRequest(CurrentUser, account.SubscriptionType);
                    return RedirectToAction("AddCredit", "Payment",account);
                }

            }

            return View("Instruct", instruction);
        }

        [HttpGet]
        public IActionResult ReIssueInstruction(Guid id)
        {
            var instruction =
                _instructionService.GetInstruction(CurrentUser, id);
            return View(instruction);
        }

        [HttpPost]
        public IActionResult ReIssueInstruction(InstructionModel instruction)
        {
            if (ModelState.IsValid)
            {
                instruction.IssuerId = CurrentUser.Id;
                _instructionService.ReIssueInstruction(instruction);
                return RedirectToAction("PendingInstruction");
            }
            return View(instruction);
        }

        [HttpGet]
        [Route("[controller]/pending")]
        public IActionResult PendingInstruction()
        {
            var statusFilters = new List<InstructionStatus>
            {
                InstructionStatus.New,
                InstructionStatus.Pending,
                InstructionStatus.Processing,
                InstructionStatus.Confirmed
            };
            var instructions =
                _instructionService.GetInstructions(CurrentUser, statusFilters);
            return View(instructions.OrderByDescending(i => i.CreatedDate));
        }

        [HttpGet]
        [Route("[controller]/rejected")]
        public IActionResult RejectedInstructions()
        {
            var instructions = _instructionService
                    .GetInstructions(CurrentUser, new List<InstructionStatus> { InstructionStatus.Rejected });
            return View(instructions);
        }

        [HttpGet]
        [Route("[controller]/completed")]
        public IActionResult CompleteInstructions()
        {
            var instructions =
                _instructionService.GetInstructions(CurrentUser, new List<InstructionStatus> { InstructionStatus.Completed });
            return View(instructions);
        }

        public IActionResult DetailedReport()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult InternalReport()
        {
            return View();
        }

        public IActionResult Administration()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            return View();
        }

        public IActionResult PropertyInformation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PropertySearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PropertySearch(PropertySearchRequest request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                request.PropertySearchType = PropertySearchType.Corporate;
                var result = await _valuationsService.PropertySearch(request, cancellationToken);
                return View("PropertySearch", result);
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> PropertySearchDetails(Guid id)
        {
            var result = await _valuationsService.GetPropertySearchDetails(id);
            return View("PropertySearchDetails", result);
        } 

        public IActionResult Valuations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DownloadSample()
        {
            if (ModelState.IsValid)
            {
                var fileName = "Instant.xlsx";
                string contentRootPath = _hostingEnvironment.WebRootPath;
                return PhysicalFile(Path.Combine(contentRootPath, fileName), "text/plain", fileName);
            }
            return View();
        }


        [HttpGet]
        public IActionResult InviteUsers()
        {
            return View();
        }

        [HttpPost]
        [Route("{controller}/adduser")]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            if (ModelState.IsValid )
            {
                return Ok(await _mediator.Send(command));
            }
            return BadRequest(ModelState.Values);
        }

        [HttpGet]
        public IActionResult InstantValuation()
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult ImportInstantValuation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ImportInstantValuationFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportInstantValuationFile([FromForm]ImportInstantValuationCommand command)
        {
            if (ModelState.IsValid)
            {
                command.RequestDate = DateTime.UtcNow;
                command.RequestUri = Request.GetDisplayUrl();
                command.Host = Request.Host.Host;

                var response = await _mediator.Send(command);
                if (response.FailedData != null)
                {
                    return View("ImportInstantValuation", response);
                }
                return View("InstantValuationBulkView", response.ComparablesResults.ToList());
            }

            return View(command);
        }
        [HttpGet]
        public IActionResult InstantValuationBulkView()
        {
            return View();
        }


        [HttpGet] 
        public async Task<IActionResult> Landrates()
        {
            var landRates = await _marketInformationService.GetLandRates();
            return View(landRates);
        }

        [HttpPost]
        public async Task<IActionResult> InstantValuationResult(string comparables = null)
        {
            if (comparables != null)
            {
                List<ComparableRequestViewModel> comparablesRequests = JsonConvert.DeserializeObject<List<ComparableRequestViewModel>>(comparables);
                if (!ModelState.IsValid && comparablesRequests.Count <= 0)
                {
                    ModelState.AddModelError("Missing data", "Please ensure all fields are filled in correctly.");
                    return View("InstantValuation");
                }

                foreach (var comparable in comparablesRequests)
                {
                    AddHttpRequestDate(comparable);
                }
                var results = (await _valuationsService.CalculateValuation(comparablesRequests)).Item1;
              
                return View("InstantValuationResultView", results);
            }
            return View("InstantValuation");
        }

        [HttpPost]
        public IActionResult InstantValuationResultView(List<ComparableResultViewModel> results)
        {
            return View(results);
        }

        [HttpGet]
        public IActionResult BuildingInformation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BuildingCosts()
        {
            var result = await _mediator.Send(new GetCurrentBuildingCostsRequest());
            return View(result);
        }

        public IActionResult PerformanceReport()
        {
            return View();
        }

        public IActionResult TotalInstructionsSummary()
        {
            var result = GetInstructionDataList();
            var dataList = JsonConvert.SerializeObject(result);

            ViewBag.Data = dataList;
            return Json(result);
        }


        public IActionResult InstructionIssuedPerValuer()
        {
            var result = _instructionService
                .GetCorporateUserInstructions(CurrentUser).ToList();
            return Json(result);
        }

        private List<GraphInstructionCountViewModel> GetInstructionDataList()
        {
            var result = _instructionService
                .GetUserInstructions(CurrentUser);

            int pending = 0;
            int complete = 0;
            int newstatus = 0;
            int rejected = 0;
            if (result != null)
            {
                foreach (var inst in result)
                {
                    if (inst.Status == InstructionStatus.Pending)
                        pending++;
                    if (inst.Status == InstructionStatus.Completed)
                        complete++;
                    if (inst.Status == InstructionStatus.New)
                        newstatus++;
                    if (inst.Status == InstructionStatus.Rejected)
                        rejected++;
                }
            }

            List<GraphInstructionCountViewModel> data = new List<GraphInstructionCountViewModel>
            {
                new GraphInstructionCountViewModel { Description = "Pending", Count = pending },
                new GraphInstructionCountViewModel { Description = "New", Count = newstatus },
                new GraphInstructionCountViewModel { Description = "Complete", Count = complete },
                new GraphInstructionCountViewModel { Description = "Rejected", Count = rejected }
            };

            return data;
        }

        private void AddHttpRequestDate(ComparableRequestViewModel comparableRequestRequest)
        {
	        comparableRequestRequest.RequestDate = DateTime.UtcNow;
	        comparableRequestRequest.RequestUri = Request.GetDisplayUrl();
	        comparableRequestRequest.Host = Request.Host.Host;
        }

        
    }

    
}