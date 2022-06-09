using AV.Contracts.Enums;
using System;

namespace AV.Common.Entities
{
    public class PropertyFeature
    {
        public Guid Id { get; set; }
        public FeatureType FeatureType{ get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
    }
}