using System.Collections.Generic;
using MediatR;

namespace AV.Contracts.Models.Accounts.Subscriptions.Command
{
    public class GetPackagesRequest : IRequest<ICollection<PackageModel>>
    {

    }
}