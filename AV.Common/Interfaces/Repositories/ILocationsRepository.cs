using System.Threading.Tasks;
using AV.Common.DTOs;
using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface ILocationsRepository : IRepository<Location>
    {
        Locality AddLocality(string localityName);

        Locality AddLocality(int locationId, string localityName);

        Locality AddLocality(Locality locality);

        Location AddLocation(string locationName);

        Location AddLocation(Location location);

        Street AddStreetName(int locationId, int localAreaId, string streetName);
        Task<LocationDetail> CreateLocationGroup(string countryName, string districtName, string locationName,
            string localityName);
        Task<Country> GetCountry(int countryId);
        Task SaveChangesAsync();
        
        Task<Locality> GetLocality(int localityId);
        
        Task<Location> GetLocation(int locationId);
        Task<District> GetDistrict(int districtId);
        Task<int> GetLocationId(string locationName);
        Task<string[]> GetLocalitiesOfLocation(int locationId);
    }
}