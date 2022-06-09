using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Models
{
    public class LocationViewModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [Required]
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<LocalityViewModel> Localities { get; set; }
        public bool Verified { get; set; }
    }
}