using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GoSmartValue.Web.Areas.api.Valuations
{
    /// <summary>
    /// Valuations end points for standard users
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = ApiConstants.AuthenticationSchemes)]
    public class StandardValuationsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _map;
        private readonly IInstructionService _instructionService;
        private readonly IValuationsService _valuationsService;
        private readonly IMediator _mediator;
        private readonly ILogger<StandardValuationsController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="map"></param>
        /// <param name="instructionService"></param>
        /// <param name="valuationsService"></param>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public StandardValuationsController(
            UserManager<User> userManager,
            IMapper map,
            IInstructionService instructionService,
            IValuationsService valuationsService,
            IMediator mediator,
            ILogger<StandardValuationsController> logger)
        {
            _userManager = userManager;
            _map = map;
            _instructionService = instructionService;
            _valuationsService = valuationsService;
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a Instant Report request for the currently logged in user
        /// </summary>
        /// <param name="comparable"></param>
        /// <returns></returns>
        [HttpPost("/api/StandardValuations/ValuationRequest")]
        [HttpPost("api/reports/instant/request")]
        public async Task<IActionResult> InstantReportRequest([FromBody] CreateInstantReportCommand instantReportCommand)
        {
            if (instantReportCommand == null)
            {
                return BadRequest(new ApiResponse<string>(null, false, "No data was submitted"));
            }

            try
            {
                _logger.LogInformation($"Instant report request received.", instantReportCommand);
                var currentUser = _map.Map<AV.Contracts.Models.Users.UserModel>(await GetCurrentUser());

                var request = new InstantReportRequest(currentUser, instantReportCommand);
                return Ok(new ApiResponse<PaymentTrack>(await _mediator.Send(request), true, "Valuation request sent"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to create Instant report for user.", ex);
                return BadRequest(new ApiResponse<string>(null, false, "Failed to save data."));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/StandardValuations/GetInstantReport/{id}")]
        [HttpGet("api/reports/instant/{id}")]
        public async Task<IActionResult> GetInstantReport(Guid id)
        {
            //check if user has paid or has subscription
            //get comparable
            var result = await _valuationsService.PerformStandardUserComparable(id);
            if (result == null)
            {
                return BadRequest(new ApiResponse<string>(null, false, "Record not found or the report has not been paid for"));
            }
            return Ok(new ApiResponse<InstantReportViewModel>(result, true, ""));
        }

        [HttpPost("/api/StandardValuations/IssueInstruction")]
        [HttpPost("api/reports/instruction")]
        public async Task<IActionResult> IssueInstruction(InstructionModel instruction)
        {
            if (instruction == null)
            {
                return BadRequest(new ApiResponse<string>(null, false, "No data was submitted"));
            }

            try
            {
                var request = new InstantReportRequest(_map.Map<AV.Contracts.Models.Users.UserModel>(await GetCurrentUser()), null, instruction);
                return Ok(await _mediator.Send(request));
                // return Ok(new ApiResponse<PaymentTrack>(await _mediator.Send(request), true, "Valuation request sent"));
            }
            catch
            {
                return BadRequest(new ApiResponse<string>(null, false, "Failed to save data"));
            }

        }

        /// <summary>
        /// Retrieves valuation by instruction id
        /// </summary>
        /// <param name="instructionId"></param>
        /// <returns></returns>
        [HttpGet("/api/StandardValuations/GetDetailedReport")]
        [HttpGet("api/reports/instruction/{instructionId}")]
        public IActionResult GetDetailedReport(Guid instructionId)
        {
            var valuation = _valuationsService.GetStandardValuationReport(instructionId);
            return Ok(new ApiResponse<DetailedReportViewModel>(valuation, true, ""));
        }

        /// <summary>
        /// Retrieves all the currently logged in users instant reports
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/StandardValuations/InstantReportList")]
        [HttpGet("api/reports/instant/currentUser")]
        public async Task<IActionResult> GetInstantReportPreView()
        {
            var currentUser = await GetCurrentUser();
            var reportPreViews = _valuationsService.GetInstantReportPreView(currentUser.Id);
            return Ok(new ApiResponse<IList<GetInstantReportPreView>>(reportPreViews, true, ""));
        }
        /// <summary>
        /// Retrieves all instructions for the currently logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/StandardValuations/InstructionList")]
        [HttpGet("api/reports/instructions/currentUser")]
        public async Task<IActionResult> GetInstructionList()
        {
            var currentUser = await GetCurrentUser();
            var instructions = await _instructionService.GetStandardUserInsructions(currentUser.Id);
            return Ok(new ApiResponse<IList<GetStandardUserInsructionViewModel>>(instructions, true, ""));
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity?.Name);
        }
    }
}