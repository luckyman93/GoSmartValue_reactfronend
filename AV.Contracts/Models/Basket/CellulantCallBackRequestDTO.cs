using System;

namespace AV.Contracts.Models.Basket;

public class CellulantCallBackRequestDTO
{
    public int checkoutRequestID { get; set; }
    public string merchantTransactionID { get; set; }
    public int requestStatusCode { get; set; }
    public string requestStatusDescription { get; set; }
    public string MSISDN { get; set; }
    public string serviceCode { get; set; }
    public string accountNumber { get; set; }
    public string currencyCode { get; set; }
    public decimal amountPaid { get; set; }
    public string requestCurrencyCode { get; set; }
    public string requestAmount { get; set; }
    public string requestDate { get; set; }
    public Object payments { get; set; }
    public int serviceChargeAmount  { get; set; }
    public decimal originalRequestAmount  { get; set; }
    public string originalRequestCurrencyCode   { get; set; }
    public Object failedPayments   { get; set; }
}