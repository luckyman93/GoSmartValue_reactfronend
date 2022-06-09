using System.Xml.Serialization;

namespace AV.Infrastructure.Services.Dpo
{
    [XmlRoot(ElementName = "Service")]
    public class Service
    {
        [XmlElement(ElementName = "ServiceType")]
        public string ServiceType { get; set; }
        [XmlElement(ElementName = "ServiceDescription")]
        public string ServiceDescription { get; set; }
        [XmlElement(ElementName = "ServiceDate")]
        public string ServiceDate { get; set; }
    }

    [XmlRoot(ElementName = "Services")]
    public class Services
    {
        [XmlElement(ElementName = "Service")]
        public Service Service { get; set; }
    }

}