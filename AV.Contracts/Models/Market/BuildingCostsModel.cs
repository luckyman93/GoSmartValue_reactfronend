using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Market
{
    public class BuildingCostsModel
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public decimal? Rate { get; set; }
		public Metric Metric { get; set; }
		public string PropertyType { get; set; }
		public decimal? StandardSize { get; set; }
	}
}
