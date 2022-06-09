using AV.Contracts.Models.Valuation;
using System;

namespace AV.Contracts.Models
{
    public class CompanyLogoDocumentModel : DocumentModel
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}