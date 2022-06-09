using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models
{
    public class Locality
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int LocationId { get; set; }
        public bool Verified { get; set; }
        public ICollection<Street> Streets { get; set; }
    }
}