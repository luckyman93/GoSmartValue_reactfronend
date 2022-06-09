using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Services;
using GoSmartValue.Web.Models.GraphModels;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.Valuer)]
    public class ValuerController : SecureController
    {
        private readonly IDocumentStoreService _documentStoreService;
        private IValuationsService _valuationsService;
        private readonly IMarketInformationService _marketInformationService;
        private readonly IInstructionService _instructionService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public ValuerController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IDocumentStoreService documentStoreService,
            IValuationsService valuationsService,
            IMarketInformationService marketInformationService,
            IInstructionService instructionService,
            IUserManagerService usersService,
            IMediator mediator,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
            _documentStoreService = documentStoreService;
            _valuationsService = valuationsService;
            _marketInformationService = marketInformationService;
            _instructionService = instructionService;
            _mediator = mediator;
            _mapper = mapper;
            _logger = new LoggerFactory().CreateLogger(typeof(ValuerController));
        }

        public Task<User> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewInstructions()
        {
            var instructions = _instructionService
                .GetInstructions(CurrentUser,
                    new List<InstructionStatus> { InstructionStatus.New, InstructionStatus.Pending });
            return View(instructions.OrderByDescending(i => i.CreatedDate));
        }

        [HttpGet]
        public IActionResult Pending()
        {
            var instructions = _instructionService
                .GetInstructions(CurrentUser,
                    new List<InstructionStatus> { InstructionStatus.Confirmed, InstructionStatus.Processing });
            return View(instructions.OrderByDescending(i => i.CreatedDate));
        }

        [HttpGet]
        public IActionResult CompleteInstructions()
        {
            var instructions = _instructionService
                .GetInstructions(CurrentUser,
                    new List<InstructionStatus> { InstructionStatus.Completed });
            return View(instructions.OrderByDescending(i => i.CreatedDate));
        }

        public IActionResult RejectedInstructions()
        {
            var instructions = _instructionService
                .GetInstructions(CurrentUser,
                    new List<InstructionStatus> { InstructionStatus.Rejected });
            return View(instructions.OrderByDescending(i => i.CreatedDate));
        }

        [HttpGet]
        public IActionResult Instruct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Instruct(InstructionModel instruction,
            ICollection<IFormFile> UploadDocument = null)
        {
            if (ModelState.IsValid)
            {
                var documents = _documentStoreService
                    .ConvertToInstructionDocument<InstructionDocumentModel>(UploadDocument)?.ToList();
                instruction.SupportingDocuments = _mapper.Map<ICollection<InstructionDocumentModel>>(documents);
                SetNewValuerInstructionDefaults(instruction);
                await _instructionService.CreateInstruction(instruction);
                return RedirectToAction("Pending");
            }

            return View(instruction);
        }

        [HttpGet]
        public async Task<IActionResult> Accept(Guid id)
        {
            if (await CanTransact(SubscriptionType.FastTrack))
            {
                try
                {
                    await _instructionService.AcceptInstruction(CurrentUser, id);
                }
                catch (GoSmartValueException customException)
                {
                    _logger.LogError(customException, "Failed to complete the instruction acceptance process.", null);
                }

                return RedirectToAction("NewInstructions");
            }
            else
            {
                var accountId = UserAccountId().Result;
                var request = new AV.Contracts.Models.Payment.Commands.AddCreditCommand
                {
                    AccountId = accountId,
                };

                return RedirectToAction("AddCredit", "Payment", request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RejectAsync(Guid id)
        {
            if (await CanTransact(SubscriptionType.FastTrack))
            {
                var instruction = _instructionService.GetInstruction(CurrentUser, id);
                if (instruction != null && instruction.Status == InstructionStatus.New
                                        && !instruction.ValuerAccepted)
                {
                    return View(instruction);
                }

                return RedirectToAction("NewInstructions");
            }
            else
            {
                var accountId = UserAccountId().Result;
                var request = new AV.Contracts.Models.Payment.Commands.AddCreditCommand
                {
                    AccountId = accountId,
                };

                return RedirectToAction("AddCredit", "Payment", request);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reject(InstructionModel instruction)
        {
            if (ModelState.IsValid)
            {
                await _instructionService.RejectInstruction(CurrentUser, instruction);
            }

            return RedirectToAction("NewInstructions");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            if (await CanTransact(SubscriptionType.FastTrack))
            {
                try
                {
                    await _instructionService.ConfirmInstructionAppointment(CurrentUser, id);
                    return RedirectToAction("Pending");
                }
                catch (GoSmartValueException customException)
                {
                    _logger.LogError(customException, "Failed to complete the instruction acceptance process.", null);
                }

                return RedirectToAction("NewInstructions");
            }
            else
            {
                var accountId = UserAccountId().Result;
                var request = new AV.Contracts.Models.Payment.Commands.AddCreditCommand
                {
                    AccountId = accountId,
                };

                return RedirectToAction("AddCredit", "Payment", request);
            }
        }

        public IActionResult Valuations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BuildingInformation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Value(Guid id)
        {
            var instruction = _instructionService.GetInstruction(CurrentUser, id);
            if (instruction == default)
            {
                throw new GoSmartValueException($"No instructions found for this Id: {id}");
            }

            if (instruction.ValuationId != null)
            {
                return RedirectToAction("ValueResultView",
                    _valuationsService.GetValuationResult(CurrentUser, instruction.Id));
            }

            ViewBag.Instruction = instruction;
            return View();
        }

        [HttpPost]
        public IActionResult Value(ValuationModel valuation, ICollection<IFormFile> Sitepictures = null)
        {
            if (ModelState.IsValid)
            {
                var documents = _documentStoreService
                    .ConvertToInstructionDocument<ValuationDocumentModel>(Sitepictures)?.ToList();
                valuation.SitePictures = _mapper.Map<ICollection<ValuationDocumentModel>>(documents);
                var valuationResult = _valuationsService.ProcessValuation(CurrentUser, valuation);
                return RedirectToAction("ValueResult", new { id = valuationResult.InstructionId });
            }

            if (valuation.Instruction == null && valuation.InstructionId != default)
            {
                var instruction = _instructionService.GetInstruction(CurrentUser, valuation.InstructionId);

                if (instruction == default)
                {
                    throw new GoSmartValueException($"No instructions found for this Id: {valuation.InstructionId}");
                }

                valuation.Instruction = instruction;
            }

            return View(valuation);
        }

        [HttpGet]
        public IActionResult ValueResultView(ValuationResultViewModel valuationResult)
        {
            return View("ValueResult", valuationResult);
        }

        [HttpGet]
        public IActionResult ValueResult([FromRoute] Guid id)
        {
            if (id != default)
            {
                var valuationResult = _valuationsService.GetValuationResult(CurrentUser, id);
                return View(valuationResult);
            }

            var message = "The record Id you have provided is not valid.";
            return RedirectToAction("Error", "Error", message);
        }

        [HttpPost]
        [Route("valuation/accept")]
        public async Task<IActionResult> AcceptValuation(decimal adjustedValue, Guid valuationId, decimal serviceFee,
            string adjustmentReason)
        {
            if (valuationId != default)
            {
                var valuationResult = _valuationsService.GetValuationResult(CurrentUser, valuationId);
                valuationResult.Valuation.Value = adjustedValue;
                valuationResult.Accepted = true;
                valuationResult.Valuation.ServiceFee = serviceFee;
                valuationResult.Valuation.AdjustmentReason = adjustmentReason;
                var result = _valuationsService.ValidateValuationAcceptance(valuationResult, CurrentUser);
                if (result.IsValid)
                {
                    await _valuationsService.AcceptValuation(valuationResult);
                    return RedirectToAction("CompleteInstructions");
                }

                ModelState.AddModelError("Accepted", "Result must be accepted.");
                return View("ValueResult", valuationResult);
            }

            return RedirectToAction("Error", "Error");
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
        public async Task<IActionResult> PropertySearch(PropertySearchRequest request,
            CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                request.PropertySearchType = PropertySearchType.Valuer;
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

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult Administration()
        {
            return View();
        }

        public IActionResult AccountHistory()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        public IActionResult Discussions()
        {
            return View();
        }

        public IActionResult PostView()
        {
            return View();
        }

        public async Task<IActionResult> ManageAccount()
        {
            var getManageAccountDataRequest = new GetManageAccountDataRequest
            {
                UserId = CurrentUser.Id,
                AccountType = AccountType.Valuer
            };
            return View(await _mediator.Send(getManageAccountDataRequest));
        }

        /// <summary>
        /// Used as an API end point for adding bank account details on currently logged inn valuers account
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{controller}/bankdetails")]
        public async Task<IActionResult> BankDetails(AddBankDetailsCommand command)
        {
            if (ModelState.IsValid && command.AccountId != default)
            {
                command.UserId = CurrentUser.Id;
                return Ok(await _mediator.Send(command));
            }

            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// Used as an API end point for adding account profile details on currently logged in valuers account
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{controller}/manageprofiles")]
        public async Task<IActionResult> ManageProfile(AddProfileDetailsCommand command)
        {
            if (ModelState.IsValid && command.AccountId != default)
            {
                return Ok(await _mediator.Send(command));
            }

            return BadRequest(ModelState.Values);
        }

        [HttpGet]
        public async Task<IActionResult> LandRates()
        {
            if (await CanTransact(SubscriptionType.FastTrack))
            {
                var landRates = await _marketInformationService.GetLandRates();
                return View(landRates);
            }
            else
            {
                var account = CurrentUser.Accounts.FirstOrDefault();
                var request = new AV.Contracts.Models.Payment.Commands.AddCreditCommand
                {
                    AccountId = account.Id,
                };

                return RedirectToAction("AddCredit", "Payment", request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuildingCosts()
        {
            if (await CanTransact(SubscriptionType.FastTrack))
            {
                var result = await _mediator.Send(new GetCurrentBuildingCostsRequest());
                return View(result);
            }
            else
            {
                var account = CurrentUser.Accounts.FirstOrDefault();
                var request = new AV.Contracts.Models.Payment.Commands.AddCreditCommand
                {
                    AccountId = account.Id,
                };

                return RedirectToAction("AddCredit", "Payment", request);
            }
        }

        public IActionResult PerformanceReport()
        {
            var result = GetInstructionDataList();
            var dataList = JsonConvert.SerializeObject(result);

            ViewBag.Data = dataList;
            return View();
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

            var data = new List<GraphInstructionCountViewModel>
            {
                new GraphInstructionCountViewModel {Description = "Pending", Count = pending},
                new GraphInstructionCountViewModel {Description = "New", Count = newstatus},
                new GraphInstructionCountViewModel {Description = "Complete", Count = complete},
                new GraphInstructionCountViewModel {Description = "Rejected", Count = rejected}
            };

            return data;
        }

        [HttpGet]
        public IActionResult SalesTrends()
        {
            return View();
        }

        private void SetNewValuerInstructionDefaults(InstructionModel instruction)
        {
            instruction.IssuerId = CurrentUser.Id;
            instruction.ValuerId = CurrentUser.Id;
            instruction.Status = InstructionStatus.Confirmed;
            instruction.ValuerAccepted = true;
            instruction.AppointmentStatus = AppointmentStatus.Confirmed;
        }
    }
}