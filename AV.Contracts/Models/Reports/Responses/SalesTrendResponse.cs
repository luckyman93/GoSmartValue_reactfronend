using System.Collections.Generic;

namespace AV.Contracts.Models.Reports.Responses
{
    public class SalesTrendResponse
    {
        public SalesTrendResponse()
        {
            Trends = new List<SalesTrends>();
        }
        public List<string> PlottingFields { get; set; }
        public List<SalesTrends> Trends { get; set; }

    }
    public class SalesTrends
    {
        public SalesTrends()
        {
            Rate = new SortedDictionary<string, decimal>();
        }
        public string Locations { get; set; }
        public int? LocationId { get; set; }

        public decimal[] Points { get; set; }
        public SortedDictionary<string, decimal> Rate { get; set; }
    }
}
