using System.Collections.Generic;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class GetAllCompaniesRequest : IRequest<ICollection<CompanyModel>>
    { }
}