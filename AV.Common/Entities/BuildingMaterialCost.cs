using AV.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class BuildingMaterialCost: BaseEntity
	{
		[Key]
		public int Id { get; set; }
		public Material Material { get; set; }
		public string Item { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public ImportHeader ImportHeader { get; set; }
	}
}
