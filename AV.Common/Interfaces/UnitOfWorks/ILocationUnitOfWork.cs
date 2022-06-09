using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface ILocationUnitOfWork : IUnitOfWork
    {
        ILocationsRepository LocationRepository { get; set; }
        ILocalAreaRepository LocalAreaRepository { get; set; }
        IEnumerable<Location> GetAllLocations();
        IEnumerable<Location> GetAllLocationData();
        IEnumerable<Locality> GetLocalAreas(int? locationId);
        Task<Country> GetCountry(int countryId);
        Locality GetLocalArea(int localAreaId);
        IEnumerable<Street> GetStreets(int localityId);
        Location GetLocation(int locationId);
        Street AddStreetName(int locationId, int localityId, string streetName);
        Locality AddLocality(int locationId, string localityName);
        Location AddLocation(string locationName);
    }
}