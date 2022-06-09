using System.Collections.Generic;
using MediatR;

namespace AV.Contracts.Models.Accounts.Subscriptions.Command
{
    public class CreatePackagesCommand: IRequest<IEnumerable<PackageModel>>
    {
        public IEnumerable<CreatePackageCommand> Packages { get; }

        public CreatePackagesCommand(IEnumerable<CreatePackageCommand> packages)
        {
            Packages = packages;
        }
    }
}