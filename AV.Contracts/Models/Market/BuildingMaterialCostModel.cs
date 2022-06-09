using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Market
{
    public class BuildingMaterialCostModel
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public Material	Material { get; set; }
		public string Item { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
