using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using FluentValidation;

namespace AV.Handlers.Market.Validators
{
    public class LandRateValidator : AbstractValidator<LandRate> 
    {
        public ILocationsRepository LocationsRepository { get; }

        public LandRateValidator(ILocationsRepository locationsRepository)
        {
            LocationsRepository = locationsRepository;
            RuleFor(x => x.CountryId)
                .MustAsync(async (id, cancellation) => await CountryExists(id))
                .WithMessage(x => $"Country with supplied ID (#LocationId: {x.CountryId}) does not exist.");
            RuleFor(x => x.DistrictId)
                .MustAsync(async (id, cancellation) => await DistrictExists(id))
                .WithMessage(x => $"District with supplied ID (#DistrictId: {x.DistrictId}) does not exist.");
            RuleFor(x => x.LocationId)
                .MustAsync(async (id, cancellation) => await LocationExists(id))
                .WithMessage( x => $"Location with supplied ID (#LocationId: {x.LocationId}) does not exist.");
        }

        private async Task<bool> LocationExists(int? id)
        {
            return id != null && await LocationsRepository.GetLocation(id.Value) != default;
        }        
        
        private async Task<bool> DistrictExists(int? id)
        {
            return id != null && await LocationsRepository.GetDistrict(id.Value) != default;
        }

        private async Task<bool> CountryExists(int id)
        {
            return await LocationsRepository.GetCountry(id) != default;
        }
    }

}
