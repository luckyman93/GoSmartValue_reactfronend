using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.Models
{
    public class LocalityViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int LocationId { get; set; }
        public bool Verified { get; set; }
        public ICollection<StreetViewModel> Streets { get; set; }
    }
}