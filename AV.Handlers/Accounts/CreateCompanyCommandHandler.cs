using AutoMapper;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Persistence.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Accounts
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyModel>
    {
        private readonly IStore<Common.Entities.Company> _companyStore;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(IStore<AV.Common.Entities.Company> companyStore, IMapper mapper)
        {
            _companyStore = companyStore;
            _mapper = mapper;
        }
        public async Task<CompanyModel> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyStore.Create(CreateCompanyEntity(request));
            return _mapper.Map<CompanyModel>(company);
        }

        private Common.Entities.Company CreateCompanyEntity(CreateCompanyCommand request)
        {
            return new Common.Entities.Company
            {
                Name = request.Name,
                Description = request.Description,
                LogoUrl = request.LogoUrl,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RegistrationNumber = request.RegistrationNumber,
                Type = (Contracts.Enums.CompanyType)request.Type

            };
        }
    }
}
