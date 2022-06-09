using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Reports.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Reports
{
    public class GetSalesTrendRequestHandler : IRequestHandler<SalesTrendRequest, SalesTrendResponse>
    {
        private readonly IComparableRepository _comparableRepository;
        public GetSalesTrendRequestHandler(IComparableRepository comparableUnitOfWork)
        {
            _comparableRepository = comparableUnitOfWork;
        }
        public async Task<SalesTrendResponse> Handle(SalesTrendRequest request, CancellationToken cancellationToken)
        {
            //fix angular date range
            request.StartDate = request.StartDate.AddDays(1);
            //determine if yearly or monthly
            YearOrMonthly yearOrMonthly = IsYearlyOrMonthly(request.StartDate, request.EndDate);

            var saleAvarages = _comparableRepository.GetAll()
                .Where(x => x.SalePrice > 0
                            && x.DateOfSale != null
                            && x.DataState == DataState.Verified
                            && x.DateOfSale >= request.StartDate
                            && x.DateOfSale <= request.EndDate)
                .Include(x => x.Location)
                .GroupBy(x => new { x.LocationId, x.Location.Name, x.DateOfSale.Value.Month, x.DateOfSale.Value.Year })
                .Select(x => new
                {
                    localitionId = x.Key.LocationId,
                    locationName = x.Key.Name,
                    year = x.Key.Year,
                    month = x.Key.Month,
                    averageSalePrice = x.Average(s => s.SalePrice ?? 0),
                })
                .OrderBy(x => x.locationName)
                .ThenBy(x => x.year)
                .ThenBy(x => x.month)
                .Distinct()
                .AsNoTracking();

            SalesTrendResponse response = new SalesTrendResponse();

            //set monthly trend for each location
            foreach (var item in saleAvarages)
            {
                var exist = response.Trends.FirstOrDefault(x => x.LocationId == item.localitionId);
                if (exist == null)
                {
                    SalesTrends resp = new SalesTrends();
                    resp.Rate.Add($"{MonthString[item.month]}/{item.year}", item.averageSalePrice);
                    resp.Locations = item.locationName;
                    resp.LocationId = item.localitionId;
                    response.Trends.Add(resp);
                    continue;
                }
                exist.Rate.Add($"{MonthString[item.month]}/{item.year}", item.averageSalePrice);

            }

            //set ploting point for the x-axis
            response.PlottingFields = PlotingFields(request.StartDate, request.EndDate);

            //allocate each trend to its respective points
            foreach (var item in response.Trends)
            {
                if (item.LocationId != null)
                {
                    decimal lastAverage = 0;// last valid average
                    MonthOrYearCounter = 12;// set counter to search back for 12 months till it gets a value
                    Month = -1;// set to previous month
                    while (lastAverage == 0 && MonthOrYearCounter > 0)
                    {
                        lastAverage = await GetStartValue(item.LocationId.Value, request.StartDate, Month);//get previous month average
                        MonthOrYearCounter--;
                        Month--;
                    }
                    item.Rate.Add("-1", lastAverage);
                }

                //set point for location response.Trends[trend]
                item.Points = new decimal[response.PlottingFields.Count];
                bool isFirstRun = true;
                decimal currentPrev = 0;
                for (int fieldPoint = 0; fieldPoint < response.PlottingFields.Count; fieldPoint++)
                {
                    decimal rate = 0;

                    if (isFirstRun)
                    {
                        isFirstRun = false;
                        //calculate initial rate
                        item.Points[fieldPoint] = CalculateRate(item.Rate["-1"], item.Rate.TryGetValue(response.PlottingFields[fieldPoint], out rate) ? rate : 0);
                        currentPrev = item.Rate["-1"];
                    }
                    else
                    {
                        if (item.Rate.TryGetValue(response.PlottingFields[fieldPoint - 1], out rate))
                        {
                            currentPrev = item.Rate[response.PlottingFields[fieldPoint - 1]];
                        }

                        item.Points[fieldPoint] = CalculateRate(currentPrev, item.Rate.TryGetValue(response.PlottingFields[fieldPoint], out rate) ? rate : 0);
                        //carry rate forward if they are no sales on the current month
                        if (item.Points[fieldPoint] == -100)
                        {
                            item.Points[fieldPoint] = item.Points[fieldPoint - 1];
                        }
                    }
                }
            }

            return response;

        }



        int MonthOrYearCounter;
        int Month = -1;
        private async Task<decimal> GetStartValue(int localityId, DateTime startDate, int month)
        {
            decimal previousAmount = 0;

            var previousPeriod = startDate.AddMonths(month);

            var startValue = await _comparableRepository
                .Find(x => x.SalePrice > 0 && x.DataState == DataState.Verified && x.LocalityId == localityId)
                .Where(x => x.DateOfSale.Value.Month == previousPeriod.Month &&
                            x.DateOfSale.Value.Year == previousPeriod.Year)
                .GroupBy(x => new { x.DateOfSale.Value.Month })
                .Select(x => new { avarageSalePrice = x.Average(s => s.SalePrice), month = x.Key.Month })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (startValue != null)
            {
                return startValue.avarageSalePrice.Value;
            }
            return previousAmount;
        }

        private decimal CalculateRate(decimal previousMonthAverage, decimal currentMonthAverage)//(b-a)/a*100%
        {
            try
            {
                return (currentMonthAverage - previousMonthAverage) / previousMonthAverage * 100;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private YearOrMonthly IsYearlyOrMonthly(DateTime startDate, DateTime endDate)
        {
            TimeSpan dateDiff = endDate - startDate;
            if (dateDiff.TotalDays > 365)
                return YearOrMonthly.yearly;
            else return YearOrMonthly.monthly;
        }
        private enum YearOrMonthly
        {
            monthly = 0, yearly = 1
        }

        private List<string> PlotingFields(DateTime startDate, DateTime endDate)
        {

            List<string> fields = new List<string>();
            while (startDate <= endDate)
            {
                fields.Add($"{MonthString[startDate.Month]}/{startDate.Year}");
                startDate = startDate.AddMonths(1);
            }
            return fields;
        }
        Dictionary<int, string> MonthString = new Dictionary<int, string> {
            {1,"Jan"},
            {2,"Feb" },
            {3,"Mar" },
            {4,"Apr" },
            {5,"May" },
            {6,"Jun" },
            {7,"Jul" },
            {8,"Aug" },
            {9,"Sep" },
            {10,"Oct"},
            {11,"Nov" },
            {12,"Dec" }
        };
    }
}
