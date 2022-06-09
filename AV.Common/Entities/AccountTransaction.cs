using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class AccountTransaction : BaseEntity
	{
	

		[Key]
		public int Id { get; set; }
		public Guid AccountId { get; set; }
		public virtual Account Account { get; set; }
		public decimal Amount { get; set; }
		[ForeignKey("Payment")]
		public Guid PaymentId { get; set; }
		public virtual PaymentHistory Payment { get; set; }
		public bool IsCredit { get; set; }
	}
}
