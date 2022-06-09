using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class ComparableBandSize
    {
        [Key]
        public string BandName { get; set; }
        public int LowerBandLimit { get; set; }
        public int UpperBandLimit { get; set; }
    }
}