using AV.Contracts.Enums;
using System;


namespace GoSmartValue.Web.ViewModels
{
    public class BuildingCostViewModel
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public decimal Rate { get; set; }
		public Metric Metric { get; set; }
		public string PropertyType { get; set; }
		public decimal? StandardSize { get; set; }
	}
}
