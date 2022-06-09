using AV.Common.Entities;
using AV.Handlers.Market.Command;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AV.Handlers.Market.Mappers
{
    public sealed class LandRateMap : ClassMap<ImportLandRatesCommandHandler.LandRateDto>
    {
        public LandRateMap()
        {
            Map(x => x.DistrictId).Name("DistrictId");
            Map(x => x.LocationId).Name("LocationId");
            Map(x => x.LocalityId).Name("LocalityId");
            Map(x => x.Metric).Name("Metric");
            Map(x => x.Rate).Name("Rate");
            Map(x => x.Zoning).Name("Zoning");
            Map(x => x.AveragePrice).Name("AveragePrice");
            Map(x => x.LowIncome).Name("LowIncome");
            Map(x => x.MiddleIncome).Name("MiddleIncome");
            Map(x => x.HighIncome).Name("HighIncome");
        }

        public class EnumConverter<T> : DefaultTypeConverter where T : struct
        {
            public string ConvertToString(TypeConverterOptions options, object value)
            {
                T result;
                if (Enum.TryParse<T>(value.ToString(), out result))
                {
                    return (Convert.ToInt32(result)).ToString();
                }

                throw new GoSmartValueException($"Invalid value to EnumConverter. Type: {typeof(T)} Value: {value}");
            }
        }
    }
}
