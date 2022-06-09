using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Persistence.Stores;
using MediatR;

namespace AV.Handlers.Accounts
{
    public class GetCompanyRequestHandler: IRequestHandler<GetCompanyRequest, CompanyModel>
    {
        private readonly IStore<Common.Entities.Company> _companiesStore;
        private readonly IMapper _mapper;

        public GetCompanyRequestHandler(IStore<AV.Common.Entities.Company> companyStore, IMapper mapper)
        {
            _companiesStore = companyStore;
            _mapper = mapper;
        }
        public async Task<CompanyModel> Handle(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = await _companiesStore.Get(request.Id,cancellationToken);
            return _mapper.Map<CompanyModel>(company);
        }
    }
}