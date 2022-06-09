using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
	public class BankAccount : BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
		[ForeignKey("Account")]
		public Guid AccountId { get; set; }
		public virtual Account Account { get; set; }
		public string BankName { get; set; }
		public string AccountName { get; set; }
		public string AccountNumber { get; set; }
		public string BranchName { get; set; }
		public int BranchCode { get; set; }
		public string SwiftCode { get; set; }
		public ICollection<CompanyLogoDocument> CompanyLogo { get; set; }
	}
}
