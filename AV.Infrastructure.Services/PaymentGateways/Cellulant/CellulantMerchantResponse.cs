namespace AV.Infrastructure.Services.PaymentGateways.Cellulant;

public class CellulantMerchantResponse
{
    public CellulantMerchantResponse(int checkoutRequestId, string merchantTransactionId, int statusCode,
        string statusDescription, string receiptNumber)
    {
        this.checkoutRequestID = checkoutRequestId;
        this.merchantTransactionID = merchantTransactionId;
        this.statusCode = statusCode;
        this.statusDescription = statusDescription;
        this.receiptNumber = receiptNumber;
    }

    public int checkoutRequestID { get; set; }
    public string merchantTransactionID { get; set; }
    public int statusCode { get; set; }
    public string statusDescription { get; set; }
    public string receiptNumber { get; set; }

}