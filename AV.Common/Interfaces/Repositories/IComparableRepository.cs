using System;
using AV.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.Repositories
{
    public interface IComparableRepository : IRepository<Comparable>
    {
        Task<Comparable> GetComparable(Guid id);    
        IEnumerable<Comparable> GetAllComparables();

        Task<string> UpdateComparablePaymentStatus(Guid comparableId);
        Task<ComparableResult> PerformComparable(Comparable comparableRequest);

        ComparableBandSize GetComparableBandClass(decimal plotSize);
        Task<IEnumerable<Comparable>> FetchAll(Func<Comparable, bool> predicate);
        Task<Guid> AddComparable(Comparable comparable);    
        IEnumerable<Comparable> GetComparablesByAddedBy(Guid userId);
    }
}