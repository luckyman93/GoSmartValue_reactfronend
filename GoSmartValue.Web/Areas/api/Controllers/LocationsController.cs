using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Models;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Areas.api.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly IValuationsService _valuationsService;

        public LocationsController(IValuationsService valuationsService)
        {
            _valuationsService = valuationsService;
        }

        [HttpGet]
        [Route("GetLocations")]
        public IList<LocationViewModel> GetLocations(bool verified = true)
        {
            return _valuationsService.GetLocations()
                .Where(l => l.Verified == verified)
                .OrderBy(l => l.Name)
                .ToList();

        }

        [HttpGet]
        [Route("GetLocation")]
        public LocationViewModel GetLocation(int id, string name)
        {
            var location = _valuationsService.GetLocations().SingleOrDefault(l => l.Id == id);
            if (location == default(LocationViewModel))
            {
                return new LocationViewModel
                {
                    Id = id,
                    Name = "location not found!"
                };
            }

            return location;
        }

        [HttpGet]
        [Route("location/search")]
        public JsonResult LocationSearch(string textPart, bool verified = true)
        {
            var locationsFiltered = _valuationsService.GetLocations()
                .Where(l => l.Verified == verified);
            var locations = new List<object>();
            foreach (var location in locationsFiltered)
            {
                if (location.Name.Contains(textPart, StringComparison.InvariantCultureIgnoreCase)) 
                    locations.Add(new {label = location.Name, val = location.Id});
            }
            return Json(locations);
        }

        [HttpPost]
        [Route("location")]

        public IActionResult AddLocation([FromBody]LocationViewModel location)
        {
            if (location == null)
            {
                return BadRequest("Location Name cannot be empty.");
            }

            var city = _valuationsService.GetLocations()
                .SingleOrDefault(l =>
                    string.Equals(l.Name, location.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            
            return Ok(city ?? _valuationsService.AddLocation(location));
        }

        [HttpGet]
        [Route("locality/search")]
        public JsonResult LocalitySearch(string textPart, bool verified = true)
        {
            var localitiesFiltered = _valuationsService.GetLocalities()
                .Where(l => l.Verified == verified);
            var localities = new List<object>();
            foreach (var locality in localitiesFiltered)
            {
                if (locality.Name.Contains(textPart, StringComparison.InvariantCultureIgnoreCase))
                    localities.Add(new { label = locality.Name, val = locality.Id });
            }
            return Json(localities);
        }

        [HttpGet]
        [Route("GetDistricts")]
        public async Task<ICollection<DistrictViewModel>> GetDistrictsAsync(int? countryId = 1) // country 1 is Botswana
        {
            var countryDetails = await _valuationsService.GetCountry(countryId.Value);
            return countryDetails.Districts.ToList();
        }

        [HttpGet]
        [Route("GetLocalities")]
        public IList<LocalityViewModel> GetLocalities(int? locationId, string locationName = null, bool verified = true)
        {
            if (!string.IsNullOrEmpty(locationName) && !locationId.HasValue)
            {
                locationId = _valuationsService.GetLocationByName(locationName)?.Id;
            }
            return _valuationsService.GetLocalities()
                .Where(l => l.Verified == verified && l.LocationId == locationId)
                .ToList();
        }

        [HttpPost]
        [Route("locality")]
        public IActionResult AddLocality([FromBody] LocalityViewModel locality)
        {
            if (locality == null || string.IsNullOrEmpty(locality.Name))
            {
                return BadRequest("Locality Name cannot be empty.");
            }

            var ward = _valuationsService.GetLocalities()
                .SingleOrDefault(l =>
                    string.Equals(l.Name, locality.Name.Trim(), StringComparison.OrdinalIgnoreCase));
            if (ward == default)
            {
                return Ok(_valuationsService.AddLocality(locality));
            }
            return Ok(locality);
        }

        [HttpGet]
        [Route("GetStreets")]
        public ICollection<StreetViewModel> GetStreets(int localityId, string localityName, bool verified = true)
        {
            return _valuationsService
                .GetStreets(localityId)
                .Where(l => l.Verified == verified)
                .ToList();
        }

        [HttpGet]
        [Route("GetLocalArea")]
        public LocalityViewModel GetLocalArea(int id)
        {
            return _valuationsService.GetLocalArea(id);
        }

        [HttpPost]
        [Route("GetValuation")]
        public async Task<IActionResult> GetValuation([FromBody]ComparableRequestViewModel comparableRequestRequest)
        {
            AddHttpRequestDate(comparableRequestRequest);
            var result = (await _valuationsService.CalculateValuation(comparableRequestRequest)).Item1;
            return RedirectToAction("ValuationResult", "Locations", result);
        }

        private void AddHttpRequestDate(ComparableRequestViewModel comparableRequestRequest)
        {
            comparableRequestRequest.RequestDate = DateTime.UtcNow;
            comparableRequestRequest.RequestUri = Request.GetDisplayUrl();
            comparableRequestRequest.Host = Request.Host.Host;
        }
    }
}