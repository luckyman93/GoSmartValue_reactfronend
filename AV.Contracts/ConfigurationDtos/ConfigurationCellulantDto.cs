namespace AV.Contracts.ConfigurationDtos
{
    public class ConfigurationCellulantDto
    {

        /// <summary>
        /// The service code is a unique identifier of our online service on the cellulant platform.
        /// Operations tied to your service will be referenced/filtered by this code.
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// The client code is a unique identifier of our business on the checkout platform.
        /// Together both the service code & client code will help to find and resolve issues belonging to a service of the business.
        /// </summary>
        public string ClientCode { get; set; }
        /// <summary>
        /// The IV key is one of the pair of keys used during encryption.
        /// The string will be used as an initialization vector in the encryption to secure requests.
        /// </summary>
        public string IvKey { get; set; }
        /// <summary>
        /// The secret key is the second key in the pair used in the encryption of a checkout request.
        /// The string will be used as an encryption key to create strong secure encryption.
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// The access key is used to validate your identity when you send a checkout request using the express checkout API
        /// </summary>
        public string AccessKey { get; set; }

        public string CurrencyCode { get; set; }
        public string ServiceShortName { get; set; }
        public string CallBackUrl { get; set; }
        public string FailRedirectUrl { get; set; }
        public string SuccessRedirectUrl { get; set; }
        public string PendingRedirectUrl { get; set; }
    }
}
