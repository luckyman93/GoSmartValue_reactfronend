using AV.Contracts.Enums;
using AV.Contracts.Models.Market.ResponseModels;
using MediatR;

namespace AV.Contracts.Models.Market.Requests
{
    public class GetAveragePlotSizeRequest : IRequest<GetAveragePlotSizeResponse>
    {
        public int LocationId { get; set; }
        public int? LocalityId { get; set; }
        public Zoning Zoning { get; set; }

    }
}