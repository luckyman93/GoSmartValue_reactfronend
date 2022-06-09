using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.ViewModels
{
	public class SalesTrendsViewModel
	{
		public string LocationName { get; set; }
		public Guid LocationId { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
	}
}
