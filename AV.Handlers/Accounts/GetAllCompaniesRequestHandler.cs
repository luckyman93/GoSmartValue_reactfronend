using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Persistence.Queries;
using MediatR;

namespace AV.Handlers.Accounts
{
    public class GetAllCompaniesRequestHandler: IRequestHandler<GetAllCompaniesRequest, ICollection<CompanyModel>>
    {
        private readonly IQueries<Common.Entities.Company> _companyQueries;
        private readonly IMapper _mapper;

        public GetAllCompaniesRequestHandler(IQueries<AV.Common.Entities.Company> companyQueries, IMapper mapper)
        {
            _companyQueries = companyQueries;
            _mapper = mapper;
        }
        public async Task<ICollection<CompanyModel>> Handle(GetAllCompaniesRequest request, CancellationToken cancellationToken)
        {
            var companies = await _companyQueries.FetchAll(cancellationToken);
            return _mapper.Map<ICollection<CompanyModel>>(companies);
        }
    }
}