using System;
using System.Collections.Generic;
using AV.Contracts.Models.Users;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class GetCompanyValuersRequest : IRequest<ICollection<ValuerViewModel>>
    {
        public GetCompanyValuersRequest(List<Guid> companies)
        {
            Companies = companies;

        }
        public List<Guid> Companies { get; private set; }
    }
}