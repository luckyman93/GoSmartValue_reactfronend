using System;

namespace AV.Contracts.Models.Basket;

public class CellulantPaymentResponseDTO
{
    
    public string MSISDN { get; set; }
    public string payerClientName { get; set; }
    public string cpgTransactionID { get; set; }
    public string serviceCode { get; set; }
    public string payerTransactionID { get; set; }
    

}