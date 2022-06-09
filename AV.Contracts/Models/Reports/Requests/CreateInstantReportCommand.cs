using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Valuation;
using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AV.Contracts.Models.Reports.Requests
{
    public class CreateInstantReportCommand : IRequest<PaymentTrack>
    {
        public Metric Metric { get; set; }
        public int PlotSize { get; set; }
        public LandUse LandUse { get; set; }
        public PropertyType PropertyType { get; set; }
        public int LocationId { get; set; }
        public int LocalityId { get; set; }

        public string StreetName { get; set; }
        public string PlotNo { get; set; }

        public int BathRooms { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int BedRooms { get; set; }
        public int Kitchens { get; set; }
        public int SittingRooms { get; set; }
        public ICollection<PropertyFeatureModel> Features { get; set; }
        [JsonIgnore]
        public int SubscriptionOptionId { get; set; } = 0;
    }
}
