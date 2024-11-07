using System.Xml.Serialization;

namespace ConsignmentIntegration.Models
{
    public class SenderDetails
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Address")]
        public string Address { get; set; }

        [XmlElement(ElementName = "Address2")]
        public string Address2 { get; set; }

        [XmlElement(ElementName = "Suburb")]
        public string Suburb { get; set; }

        [XmlElement(ElementName = "State")]
        public string State { get; set; }

        [XmlElement(ElementName = "Postcode")]
        public int Postcode { get; set; }

        [XmlElement(ElementName = "Reference")]
        public string Reference { get; set; }

        [XmlElement(ElementName = "Contact")]
        public string Contact { get; set; }

        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
    }
}
