using AV.Common.Entities;
using AV.Handlers.Market.Command;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AV.Handlers.Market.Mappers
{
    public class BuildingMaterialCostMap : ClassMap<ImportBuildingMaterialCostsCommandHandler.BuildingMaterialsCostDto>
    {
        public BuildingMaterialCostMap()
        {
            Map(x => x.Material).Name("Material");
            Map(x => x.Item).Name("Item");
            Map(x => x.Description).Name("Description");
            Map(x => x.Price).Name("Price");
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
