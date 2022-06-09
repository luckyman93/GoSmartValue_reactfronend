using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models.Valuation
{
    public class ComparableBandSizeViewModel
    {
        [Key]
        public string BandName { get; set; }
        [Required]
        public int LowerBandLimit { get; set; }
        [Required]
        public int UpperBandLimit { get; set; }
    }
}