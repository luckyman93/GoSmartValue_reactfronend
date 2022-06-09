using AV.Contracts.Enums;
using System;
using System.Text.Json.Serialization;

namespace AV.Contracts.Models.Valuation
{
    public class PropertyFeatureModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public FeatureType FeatureType { get; set; }
        [JsonIgnore]
        public string Description { get; set; }
        [JsonIgnore]
        public string Comment { get; set; }
    }
}