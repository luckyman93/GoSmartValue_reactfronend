using AV.Common.Entities;
using GoSmartValue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Models.GraphModels
{
	public class GraphInstructionCountViewModel
	{
		public string Description { get; set; }
		public int Count { get; set; }
	}
}
