using AV.Contracts.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AV.Common.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public ProductCategory Category { get; set; }
        public decimal? Price { get; set; }
        public string ServiceType { get; set; }
        public string SampleUrl { get; set; }
        public IEnumerable<ProductFeature> Features { get; set; }
    }
}
