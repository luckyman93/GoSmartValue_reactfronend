using AV.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class BuildingCost : BaseEntity
	{
		
		[Key]
		public int Id { get; set; }
		public decimal? Rate { get; set; }
		public Metric Metric { get; set; }
		public string PropertyType { get; set; }
		public decimal? StandardSize { get; set; }
		public ImportHeader ImportHeader { get; set; }
	}
}
