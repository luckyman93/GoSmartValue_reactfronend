using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Reports.Responses
{
    public class ValueTrendReportResponse
    {
        public ValueTrendReportResponse()
        {
            Data = new Dictionary<DateTime, int>();
        }
        public Dictionary<DateTime, int> Data { get; set; }
    }
}
