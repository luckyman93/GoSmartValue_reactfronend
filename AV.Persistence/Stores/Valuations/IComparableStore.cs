using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Persistence.Stores.Valuations
{
    public interface IComparableStore : IStore<Comparable>
    {
        Task CreateResults(List<ComparableResult> results, CancellationToken cancellationToken);
    }
}
