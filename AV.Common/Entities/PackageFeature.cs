using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class PackageFeature : Feature
    {
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

    }
}