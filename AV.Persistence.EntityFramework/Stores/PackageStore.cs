using System;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Persistence.Exceptions;
using AV.Persistence.Stores.Subscriptions;

namespace AV.Persistence.EntityFramework.Stores
{
    public class PackageStore : StoreBase<Package>, IPackageStore
    {
        public PackageStore(ValuationsContext context) : base(context)
        {
        }

        public override async Task<Package> Get(Guid id, CancellationToken cancellationToken = default)
        {
            await SetTransactionIsolationLevel(false, cancellationToken);
            var package = await DbSet.FindAsync(id, cancellationToken);
            
            return EntityNotFoundHandler.Handle(id, package);
        }
    }
}