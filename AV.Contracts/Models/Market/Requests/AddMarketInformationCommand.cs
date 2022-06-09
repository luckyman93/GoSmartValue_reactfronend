using AV.Contracts.Enums;
using MediatR;
using MarketInformation = AV.Contracts.Models.Market.ResponseModels.MarketInformation;

namespace AV.Contracts.Models.Market.Requests
{
    public class AddMarketInformationCommand : IRequest<MarketInformation>
    {
        public string CountryName { get; set; }
        public string DistrictName { get; set; }
        public string LocationName { get; set; }
        public string LocalityName { get; set; }
        public double? FromRate { get; set; }
        public double? ToRate { get; set; }
        public Zoning Zoning { get; set; }
        public double AveragePlotSize { get; set; }
        public Metric AveragePlotSizeMetric { get; set; }
        public double AveragePrice { get; set; }
    }
}
