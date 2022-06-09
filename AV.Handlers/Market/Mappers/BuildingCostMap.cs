using AV.Common.Entities;
using AV.Handlers.Market.Command;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AV.Handlers.Market.Mappers
{
	public sealed class BuildingCostMap : ClassMap<ImportBuildingCostsCommandHandler.BuildingCostDto>
	{
        public BuildingCostMap()
        {
            Map(x => x.PropertyType).Name("PropertyType");
            Map(x => x.StandardSize).Name("StandardSize");
            Map(x => x.Metric).Name("Metric");
            Map(x => x.Rate).Name("Rate");
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
