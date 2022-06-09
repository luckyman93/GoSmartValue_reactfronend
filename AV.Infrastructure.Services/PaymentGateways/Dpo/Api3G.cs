using System.Xml.Serialization;

namespace AV.Infrastructure.Services.Dpo
{
    [XmlRoot(ElementName = "API3G")]
    public class Api3G
    {

        [XmlElement(ElementName = "CompanyToken")]
        public string CompanyToken { get; set; }
        [XmlElement(ElementName = "Request")]
        public string Request { get; set; }
        public Transaction Transaction { get; set; }
        [XmlElement(ElementName = "Services")]
        public Services Services { get; set; }
        [XmlElement(ElementName = "Result")]
        public string Result { get; set; }
        [XmlElement(ElementName = "ResultExplanation")]
        public string ResultExplanation { get; set; }      
        [XmlElement(ElementName = "TransRef")]
        public string TransRef { get; set; }
        [XmlElement(ElementName = "TransToken")]
        public string TransToken { get; set; }
    }
}