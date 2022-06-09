using System;
using AV.Contracts.Models.Accounts;

namespace GoSmartValue.Web.ViewModels
{
	public class BankAccountViewModel
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public virtual AccountViewModel Account { get; set; }
		public string BankName { get; set; }
		public string AccountName { get; set; }
		public int AccountNumber { get; set; }
		public string BranchName { get; set; }
		public int BranchCode { get; set; }
		public string SwiftCode { get; set; }
        public bool IsMainAccount { get; set; }
    }
}
