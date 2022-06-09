using System;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class GetCompanyRequest : IRequest<CompanyModel>
    {
        public GetCompanyRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}