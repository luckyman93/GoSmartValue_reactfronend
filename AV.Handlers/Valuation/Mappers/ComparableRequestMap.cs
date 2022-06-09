using AV.Common.Entities;
using AV.Handlers.Valuation.Command;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace AV.Handlers.Valuation.Mappers
{
    public sealed class ComparableRequestMap : ClassMap<ImportInstantValuationCommandHandler.ComparableRequestDto>
    {
        public ComparableRequestMap()
        {
            Map(x => x.LocationName).Name("Location");
            Map(x => x.LocalityName).Name("Locality");
            Map(x => x.PlotNo).Name("PlotNo");
            Map(x => x.Size).Name("PlotSize");
            Map(x => x.PropertyType).Name("PropertyType");
            Map(x => x.LandUse).Name("LandUse");
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
