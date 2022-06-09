using AutoMapper;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Interface;
using AV.Contracts.Models.Valuation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AV.Handlers.Reports.Services
{
    public class UserReportService : IUserReportService
    {
        private readonly IComparableRepository _reportsRepository;
        private readonly IMapper _mapper;

        public UserReportService(
            IComparableRepository reportsRepository,
            IMapper mapper)
        {
            _reportsRepository = reportsRepository;
            _mapper = mapper;
        }

        public IList<GetInstantReportPreView> GetInstantReports(Guid userId)
        {
            var reports = _reportsRepository.GetComparablesByAddedBy(userId)
                .OrderByDescending(r => r.AddedOn);

            return reports
                .Select(c => _mapper.Map<GetInstantReportPreView>(c))
                .ToList();
        }

        public IList<DetailedReportViewModel> GetDetailedReports(Guid userId)
        {
            var reports = _reportsRepository.GetComparablesByAddedBy(userId)
                .Where(r => r.AddedBy == userId
                            && r.TransactionType != TransactionType.InstantReport
                            && r.DataState == DataState.Verified)
                .OrderByDescending(r => r.AddedOn);

            return _mapper.Map<IList<DetailedReportViewModel>>(reports);
        }
    }
}
