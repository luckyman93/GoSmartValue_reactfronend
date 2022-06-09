using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoles = AV.Common.Constants.UserRoles;

namespace GoSmartValue.Web.Services
{
    public class CachedStorageService
    {
        private readonly ILocationsRepository _locationsRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private static List<Account> _accounts;
        private Task<IList<User>> _valuers;
        private static IList<Location> _locations { get; set; }
        private static IList<Locality> _localities { get; set; }
        private static IList<Street> _streets { get; set; }
        private static IList<District> _districts { get; set; }

        public CachedStorageService(
            ILocationsRepository locationsRepository,
            IAccountsRepository accountsRepository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _locationsRepository = locationsRepository;
            _userManager = userManager;
            _mapper = mapper;

            Country = GetDistricts(1).Result;

            _accounts = accountsRepository.GetAll().ToList();

            District = new Dictionary<int, string>();
            Cities = new Dictionary<int, string>();
            Localities = new Dictionary<int, string>();
            Streets = new Dictionary<int, string>();

            _districts = new List<District>();
            _locations = new List<Location>();
            _localities = new List<Locality>();
            _streets = new List<Street>();

            foreach (var district in Country.Districts)
            {
                _districts.Add(district);
                District.Add(district.Id, district.Name);
                foreach (var location in district.Regions)
                {
                    _locations.Add(location);
                    Cities.Add(location.Id, location.Name);
                    foreach (var localArea in location.LocalAreas)
                    {
                        //No duplicates are welcome
                        if (Localities.Keys.Contains(localArea.Id))
                            continue;
                        _localities.Add(localArea);
                        Localities.Add(localArea.Id, localArea.Name);
                        foreach (var street in localArea.Streets)
                        {
                            //No duplicates are welcome
                            if (Streets.Keys.Contains(street.Id))
                                continue;
                            _streets.Add(street);
                            Streets.Add(street.Id, street.StreetName);
                        }
                    }
                }
            }

            _valuers = _userManager.GetUsersInRoleAsync(UserRoles.Valuer);
        }

        public static Country Country { get; set; }
        public static IDictionary<int, string> District { get; set; }
        public static IDictionary<int, string> Cities { get; set; }
        public static IDictionary<int, string> Localities { get; set; }
        public static IDictionary<int, string> Streets { get; set; }
        public static IDictionary<string, string> Valuers { get; set; }


        public static IList<PropertyType> GetDevelopments()
        {
            var developments = Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>()
                .Where(p => p != PropertyType.Unknown)
                .ToList();

            return developments;
        }
        public static SelectList GetLocalities(int locationId)
        {
            return new SelectList(
                _localities
                    .Where(l => l.LocationId == locationId)
                    .Select(c =>
                        new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                "Value", "Text", "-");
        }
        public ICollection<StreetViewModel> GeStreets(int localId)
        {
            return _mapper.Map<ICollection<StreetViewModel>>(_localities.Where(l => l.Id == localId)
                .Select(l => l.Streets));
        }

        public IEnumerable<AccountViewModel> GetAccounts(AccountType accountType)
        {
            var accounts = _accounts.Where(a => a.AccountType == accountType
                                                && !string.IsNullOrEmpty(a.CompanyName));
            return _mapper.Map<ICollection<AccountViewModel>>(accounts.OrderBy(a => a.CompanyName));
        }

        public async Task<IEnumerable<ValuerViewModel>> GetValuers()
        {
            var valuers = await _userManager.GetUsersInRoleAsync(UserRoles.Valuer);
            return _mapper.Map<ICollection<ValuerViewModel>>(valuers);
        }

        public async Task<Country> GetDistricts(int countryId)
        {
            return await _locationsRepository.GetCountry(countryId);

        }

        public async Task<string> GetLocalityName(int localityId)
        {
            if (localityId > 0 && !Localities.TryGetValue(localityId, out string localityName))
            {
                var locality = await _locationsRepository.GetLocality(localityId);
                if (locality == null)
                    return string.Empty;
                Localities.Add(locality.Id, locality.Name);
                return locality.Name;
            }
            return string.Empty;
        }

        public async Task<string> GetLocationName(int locationId)
        {
            if (!Cities.TryGetValue(locationId, out string locationName))
            {
                Location location = await _locationsRepository.GetLocation(locationId);
                Cities.Add(location.Id, location.Name);
                locationName = location.Name;
            }
            return locationName;
        }

        public async Task<int> GetLocationId(string locationName)
        {
            var locationId = await _locationsRepository.GetLocationId(locationName);
            return locationId;
        }


        public async Task<string> GetDistrictName(int districtId)
        {
            if (!District.TryGetValue(districtId, out string districtName))
            {
                District district = await _locationsRepository.GetDistrict(districtId);
                District.Add(district.Id, district.Name);
                districtName = district.Name;
            }
            return districtName;
        }
    }
}
