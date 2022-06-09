using System.Xml.Serialization;

namespace AV.Infrastructure.Services.Dpo
{
    [XmlRoot(ElementName = "Transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "PaymentAmount")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "PaymentCurrency")]
        public string PaymentCurrency { get; set; }
        [XmlElement(ElementName = "CompanyRef")]
        public string CompanyRef { get; set; }
        [XmlElement(ElementName = "RedirectURL")]
        public string RedirectURL { get; set; }
        [XmlElement(ElementName = "BackURL")]
        public string BackURL { get; set; }
        [XmlElement(ElementName = "CompanyRefUnique")]
        public byte CompanyRefUnique { get; set; }
        [XmlElement(ElementName = "PTL")]
        public string PTL { get; set; }
    }
}
