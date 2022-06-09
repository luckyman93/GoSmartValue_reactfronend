using System;
namespace AV.Contracts.Models
{
	public class BankAccount
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public virtual Account Account { get; set; }
		public string BankName { get; set; }
		public string AccountName { get; set; }
		public string AccountNumber { get; set; }
		public string BranchName { get; set; }
		public int BranchCode { get; set; }
		public string SwiftCode { get; set; }
        public bool IsMainAccount { get; set; }
    }
}
