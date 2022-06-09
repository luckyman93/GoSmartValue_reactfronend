using System;
using System.Linq;
using System.Threading.Tasks;
using AV.Contracts;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoSmartValue.Web.Areas.api.Markets;

[ApiTokenAuth]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MarketsController: ControllerBase
{
    private readonly ILogger<MarketsController> _logger;
    private readonly IMediator _mediator;
    private readonly IUserManagerService _userManagerService;
    private readonly CachedStorageService _cachedStorageService;
    
    public MarketsController(
        ILogger<MarketsController> logger,
        IMediator mediator,
        IUserManagerService userManagerService,
        CachedStorageService cachedStorageService)
    {
        _logger = logger;
        _mediator = mediator;
        _userManagerService = userManagerService;
        _cachedStorageService = cachedStorageService;
    }
    
    [HttpGet(ApiUrlConstants.Markets.GET_buildingMaterialCosts)]
    public async Task<IActionResult> GetBuildingMaterialCosts()
    {
        try
        {
            var userId = _userManagerService.GetLoggedInUserIdAsync();
            return Ok(await _mediator.Send(new GetCurrentBuildingMaterialCostsRequest()));
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to get building material costs.", exception);
            return BadRequest();
        }

    }

    [HttpGet(ApiUrlConstants.Markets.GET_landRates)]
    public async Task<IActionResult> GetLandRates()
    {
        try
        {
            var userId = _userManagerService.GetLoggedInUserIdAsync();
            return Ok(await _mediator.Send(new GetAllCurrentLandRatesRequest()));
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to get land rates.", exception);
            return BadRequest();
        }

    } 
    
    [HttpGet(ApiUrlConstants.Markets.GET_buildingCosts)]
    public async Task<IActionResult> GetBuildingCosts()
    {
        try
        {
            var userId = _userManagerService.GetLoggedInUserIdAsync();
            return Ok(await _mediator.Send(new GetCurrentBuildingCostsRequest()));
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to get building costs.", exception);
            return BadRequest();
        }

    }

    [HttpGet(ApiUrlConstants.Markets.GET_salesTrends)]
    public async Task<ActionResult<ValueTrendReportResponse>> GetSalesValueTrends([FromQuery] GetValueTrendReportRequest request)
    {
        if (request.LocationId == 0)
        {
            request.LocationId = await _cachedStorageService.GetLocationId(request.Location);
        }
        var result = await _mediator.Send(request);
        return Ok(new
        {
            Data = result.Data.ToList()
        });
    }
    
}