using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class CompanyLogoDocument : Document
    {
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}