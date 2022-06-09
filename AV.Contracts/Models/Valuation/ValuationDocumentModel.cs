using System;

namespace AV.Contracts.Models.Valuation
{
    public class ValuationDocumentModel : DocumentModel
    { 
            public Guid ValuationId { get; set; }
            public virtual ValuationModel Valuation { get; set; }
    }
}