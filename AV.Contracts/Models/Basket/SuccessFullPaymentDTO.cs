namespace AV.Contracts.Models.Basket;

public class SucceFullPaymentDTO
{
    public int checkoutRequestID { get; set; }
    public string merchantTransactionID { get; set; }
    public int statusCode { get; set; }
    public string statusDescription { get; set; }
    public string receiptNumber { get; set; }
}