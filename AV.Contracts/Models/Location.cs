using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<Locality> Localities { get; set; }
        public bool Verified { get; set; }
    }
}