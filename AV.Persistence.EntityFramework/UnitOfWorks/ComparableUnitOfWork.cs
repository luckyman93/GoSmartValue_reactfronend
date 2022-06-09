using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace AV.Persistence.EntityFramework.UnitOfWorks;

public class ComparableUnitOfWork : UnitOfWork, IComparableUnitOfWork
{
    public IComparableRepository ComparableRepository { get; set; }

    public ComparableUnitOfWork(
        IdentityDbContext<User, Role, Guid> dbContext,
        IComparableRepository comparableRepository) : base(dbContext)
    {
        ComparableRepository = comparableRepository;
    }

    public ICollection<Comparable> GetAllComparables(DataState? dataState, int skip = 0, int take = 1000)
    {
        var allSalesData = ComparableRepository.GetAll().Where(c => !c.IsDeleted);

        if (!dataState.HasValue)
        {
            return allSalesData
                .OrderByDescending(p => p.AddedOn)
                .Skip(skip).Take(take)
                .ToList();
        }
        return allSalesData
            .Where(s => s.DataState == dataState.Value)
            .OrderByDescending(p => p.AddedOn)
            .Skip(skip).Take(take)
            .ToList();
    }

    public async Task SoftDelete(Guid id)
    {
        var comparable = ComparableRepository.Get(id);
        if (comparable == null) return;

        comparable.IsDeleted = true;
        comparable.DeletedOn = DateTimeOffset.UtcNow;
        await ComparableRepository.SaveChangesAsync();
    }

    public async Task VerifyComparable(Guid id)
    {
        var comparable = ComparableRepository.Get(id);
        if (comparable == null) return;

        comparable.DataState = DataState.Verified;
        comparable.LastUpdatedOn = DateTimeOffset.UtcNow;
        await ComparableRepository.SaveChangesAsync();
    }

    public void RequestFullReport(ReportRequest reportRequest)
    {
        reportRequest.Status = RequestStatus.Pending;
        _dbContext.Set<ReportRequest>().Add(reportRequest);
        Complete();
    }

    public Comparable GetComparable(Guid comparableId)
    {
        return ComparableRepository
            .Find(p => p.Id == comparableId)
            .Include(p => p.Location)
            .Include(p => p.Locality)
            .SingleOrDefault();
    }

    public void AddProperty(Comparable comparable)
    {
        ComparableRepository.Add(comparable);
        Complete();
    }

    public void SaveComparableChanges(Comparable comparable)
    {
        comparable.PlotSize = ConvertToMetreSquared(comparable.PlotSize, comparable.Metric);
        comparable.Metric = Metric.SquareMetres;
        comparable.BandClass = GetComparableBandClass(comparable.PlotSize);
        _dbContext.Set<Comparable>().Update(comparable);
        _dbContext.SaveChangesAsync();
    }

    public async Task<ComparableResult> PerformComparable(Comparable comparableRequest)
    {
        var result = ComparableRepository.PerformComparable(comparableRequest);
        Complete();
        return await result;

    }

    public async Task<ComparableResult> GetComparableResult(Guid id)
    {
        return await _dbContext.Set<ComparableResult>()
            .Include(c => c.Comparable)
            .Include(c => c.Comparable.Location)
            .Include(c => c.Comparable.Locality)
            .Include(c => c.Comparables)
            .ThenInclude(cr => cr.ComparableResult)
            .FirstOrDefaultAsync(c => c.ReferenceNumber == id);
    }

    public IEnumerable<Comparable> GetAllComparables()
    {
        return ComparableRepository.GetAllComparables();
    }

    public ComparableBandSize GetComparableBandClass(decimal plotSize)
    {
        return ComparableRepository.GetComparableBandClass(plotSize);
    }

    public decimal ConvertToMetreSquared(decimal? plotSize, Metric metric)
    {
        if (!plotSize.HasValue) return 0;
        return plotSize.Value * Constants.GetMetricMultiplierToMeterSquared(metric);
    }
}