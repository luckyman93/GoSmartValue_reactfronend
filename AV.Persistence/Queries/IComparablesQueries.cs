using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Persistence.Queries
{
    public interface IComparablesQueries: IQueries<Comparable>
    {
        Task<IEnumerable<Comparable>> FetchAllVerified(CancellationToken token);
        Task<IEnumerable<ComparableBandSize>> GetAllComparableBandClass();
        Task<IEnumerable<LandRate>> GetAllLandRates();
    }
}
