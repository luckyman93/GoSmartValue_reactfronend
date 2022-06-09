using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using GoSmartValue.Web.Areas.api;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Controllers.Reports
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(AuthenticationSchemes=ApiConstants.AuthenticationSchemes)]
    public class GraphReports : SecureController
    {
		private readonly IMediator _mediator;
		private readonly CachedStorageService _cachedStorageService;

		public GraphReports(UserManager<User> userManager, RoleManager<Role> roleManager, IUserManagerService userService, IMapper mapper, IMediator mediator, CachedStorageService cachedStorageService) 
            : base(userManager, roleManager, userService, mapper)
        {
            _mediator = mediator;
			_cachedStorageService = cachedStorageService;
		}

        [HttpGet("salestrend")]
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

        [HttpGet("reports/salestrend")]
        public ActionResult SalesValueTrends()
        {
            return View("~/Views/Reports/SalesTrendReport.cshtml");
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<LocationsTransactionsResponse>> GetTransactingLocations(GetLocationsTransactionsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(new
            {
                Data = result.Data.ToList()
            });
        }
        [HttpPost("GetSalesTrends")]
        public async Task<ActionResult<LocationsTransactionsResponse>> GetTransactingLocationsPost([FromBody]SalesTrendRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(new
            {
                Data = result
            });
        }
    }
}
