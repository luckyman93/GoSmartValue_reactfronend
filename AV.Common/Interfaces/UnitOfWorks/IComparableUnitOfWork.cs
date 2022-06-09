using AV.Common.Entities;
using AV.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IComparableUnitOfWork : IUnitOfWork
    {
        ICollection<Comparable> GetAllComparables(DataState? dataState, int skip = 0, int take = 5000);

        Task SoftDelete(Guid id);
        Task VerifyComparable(Guid id);

        void RequestFullReport(ReportRequest reportRequest);

        void AddProperty(Comparable comparable);

        Comparable GetComparable(Guid comparableId);

        void SaveComparableChanges(Comparable comparable);

        Task<ComparableResult> PerformComparable(Comparable comparableRequest);

        decimal ConvertToMetreSquared(decimal? plotSize, Metric metric);

        Task<ComparableResult> GetComparableResult(Guid id);

        IEnumerable<Comparable> GetAllComparables();

        ComparableBandSize GetComparableBandClass(decimal plotSize);
    }
}