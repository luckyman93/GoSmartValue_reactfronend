using AV.Contracts;
using AV.Contracts.ConfigurationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoSmartValue.Web.Areas.api.Configuartion
{
    /// <summary>
    /// Valuations end points for standard users
    /// </summary>
    [ApiController]
    [Route("api/configuration")]
    [Produces("application/json")]
    [ApiTokenAuth]
    public class Configuration : Controller
    {
        private readonly IOptions<ConfigurationUiDto> _uiConfiguration;

        public Configuration(IOptions<ConfigurationUiDto> uiConfiguration)
        {
            _uiConfiguration = uiConfiguration;
        }
        [HttpGet("")]
        public ActionResult<ConfigurationUiDto> GetUiConfig()
        {
            return Ok(_uiConfiguration);
        }
    }
}
