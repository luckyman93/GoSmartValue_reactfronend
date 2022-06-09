using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class LocationsRepository : Repository<Location>, ILocationsRepository
    {
        public LocationsRepository(ValuationsContext context) : base(context)
        {
            DbContext = context;
        }

        public Country AddCountry(string countryName, bool verified = false)
        {
            var countries = GetAllCountries().ToList();
            var country = countries.FirstOrDefault(l => l.Name.Trim().ToLower() == countryName.Trim().ToLower());
            if (country != default)
                return country;
            country = new Country(countryName, verified);
            return AddCountry(country);
        }

        public Country AddCountry(Country country)
        {
            if (IsValidCountry(country))
            {
                DbContext.Set<Country>().Add(country);
            }
            return country;
        }

        public District AddDistrict(string districtName, bool verified = false)
        {
            var district = GetAllDistricts()
                .FirstOrDefault(l => string.Equals(l.Name, districtName, StringComparison.OrdinalIgnoreCase));
            if (district != default)
                return district;
            district = new District(districtName, verified);
            return AddDistrict(district);
        }

        public District AddDistrict(District district)
        {
            if (IsValidDistrict(district))
            {
                DbContext.Set<District>().Add(district);
            }
            return district;
        }

        public Location AddLocation(Location location)
        {
            if (IsValidLocation(location))
            {
                DbContext.Set<Location>().Add(location);
            }
            return location;
        }

        public Location AddLocation(string locationName)
        {
            var location = GetAll()
                 .SingleOrDefault(l => string.Equals(l.Name, locationName));
            if (location != default)
                return location;
            location = new Location(locationName);
            return AddLocation(location);
        }

        public Locality AddLocality(string localityName)
        {
            var locality = DbContext.Set<Locality>()
                .SingleOrDefault(l => string.Equals(l.Name, localityName, StringComparison.OrdinalIgnoreCase));
            if (locality != default) return locality;

            locality = new Locality
            {
                Name = localityName
            };

            return AddLocality(locality);
        }

        public Locality AddLocality(int locationId, string localityName)
        {
            var locality = DbContext.Set<Locality>()
                .FirstOrDefault(l =>
                    l.LocationId == locationId
                    && string.Equals(l.Name, localityName, StringComparison.OrdinalIgnoreCase));
            if (locality != default)
                return locality;

            locality = new Locality(locationId, localityName);
            return AddLocality(locality);
        }

        public Locality AddLocality(Locality locality)
        {
            if (IsValidLocality(locality))
            {
                DbContext.Set<Locality>().Add(locality);
            }
            return locality;
        }

        public Street AddStreetName(int locationId, int localAreaId, string streetName)
        {
            if (string.IsNullOrEmpty(streetName))
            {
                Debug.WriteLine("StreetName value cannot be saved because it is empty.");
                return null;
            }

            var streetNameFromStorage = DbContext.Set<Street>()
                .Where(c => c.StreetName.Trim()
                               .Equals(streetName.Trim(), StringComparison.CurrentCultureIgnoreCase)
                                                                           && c.LocalityId == localAreaId);

            if (streetNameFromStorage.Any())
            {
                Debug.WriteLine($"StreetName: {streetName} has already been added to storage. Not Saved.");
                return streetNameFromStorage.SingleOrDefault();
            }

            var street = new Street
            {
                LocalityId = localAreaId,
                StreetName = streetName.Trim(),
            };
            DbContext.Set<Street>().Add(street);
            DbContext.SaveChanges();
            DbContext.Entry(street).Reload();
            return street;
        }

        public async Task<LocationDetail> CreateLocationGroup(string countryName, string districtName, string locationName, string localityName)
        {
            var locationDetail = new LocationDetail
            {
                Country = AddCountry(countryName),
                District = !string.IsNullOrEmpty(districtName) ? AddDistrict(districtName) : null,
                Location = !string.IsNullOrEmpty(locationName) ? AddLocation(locationName) : null,
                Locality = !string.IsNullOrEmpty(localityName) ? AddLocality(localityName) : null
            };

            if (locationDetail.District != null)
            {
                locationDetail.Country.Districts.Add(locationDetail.District);
                if (locationDetail.Location != null)
                {
                    locationDetail.District.Regions.Add(locationDetail.Location);
                    if (locationDetail.Locality != null)
                    {
                        locationDetail.Location.LocalAreas.Add(locationDetail.Locality);
                    }
                }
            }

            await SaveChangesAsync();

            return locationDetail;
        }

        public async Task<Country> GetCountry(int countryId)
        {
            return await DbContext.Set<Country>()
                .AsNoTracking()
                .Where(c => c.Id == countryId)
                .Include(c => c.Districts)
                .ThenInclude(c => c.Regions)
                .ThenInclude(c => c.LocalAreas)
                .ThenInclude(c => c.Streets)
                .FirstOrDefaultAsync();
        }

        public async Task<Locality> GetLocality(int localityId)
        {
            return await DbContext.Set<Locality>()
                .AsNoTracking()
                .Where(c => c.Id == localityId)
                .Include(c => c.Streets)
                .FirstOrDefaultAsync();
        }

        public async Task<Location> GetLocation(int locationId)
        {
            return await DbContext.Set<Location>()
                .AsNoTracking()
                .Where(c => c.Id == locationId)
                .Include(c => c.LocalAreas)
                .ThenInclude(l => l.Streets)
                .FirstOrDefaultAsync();
        }

        public async Task<District> GetDistrict(int districtId)
        {
            return await DbContext.Set<District>()
                .AsNoTracking()
                .Where(c => c.Id == districtId)
                .Include(c => c.Regions)
                .ThenInclude(l => l.LocalAreas)
                .ThenInclude(l => l.Streets)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Country> GetAllCountries()
        {
            return DbContext.Set<Country>().AsNoTracking();
        }

        private IQueryable<District> GetAllDistricts()
        {
            return DbContext.Set<District>();
        }

        private bool IsValidCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrEmpty(country.Name))
                throw new DataException("validation failed: Country name is empty.");

            return true;
        }
        private bool IsValidDistrict(District district)
        {
            if (district == null)
                throw new ArgumentNullException(nameof(district));
            if (string.IsNullOrEmpty(district.Name))
                throw new DataException("validation failed: District name is empty.");

            return true;
        }

        private bool IsValidLocation(Location location)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));
            if (string.IsNullOrEmpty(location.Name))
                throw new DataException("validation failed: Location name is empty.");

            return true;
        }

        private bool IsValidLocality(Locality locality)
        {
            if (locality == null)
                throw new ArgumentNullException(nameof(locality));
            if (string.IsNullOrEmpty(locality.Name))
                throw new DataException("validation failed: Locality name is empty.");

            return true;
        }

        public async Task<int> GetLocationId(string locationName)
        {
            var result = await DbContext.Set<Location>()
                 .Where(c => c.Name == locationName
                 && c.Verified == true)
                 .FirstOrDefaultAsync();

            return result.Id;
        }

        public async Task<string[]> GetLocalitiesOfLocation(int locationId)
        {
            return await DbContext.Set<Locality>()
               .Where(c => c.LocationId == locationId)
                .Select(x => x.Name).ToArrayAsync();
        }
    }
}
