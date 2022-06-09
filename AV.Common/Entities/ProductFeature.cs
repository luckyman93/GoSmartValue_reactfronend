using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class ProductFeature : Feature
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}