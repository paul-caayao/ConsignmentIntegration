﻿using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ConsignmentIntegration.Models
{
    [XmlRoot("Consignment")]
    public class Consignment
    {
        [XmlElement(ElementName = "UniqueId")]
        public double UniqueId { get; set; }

        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "Date")]
        public DateTime Date { get; set; }

        [XmlElement(ElementName = "CustomerCode")]
        public string CustomerCode { get; set; }

        [XmlElement(ElementName = "SenderDetails")]
        public SenderDetails SenderDetails { get; set; }

        [XmlElement(ElementName = "ConsignmentPickupSpecialInstructions")]
        public string ConsignmentPickupSpecialInstructions { get; set; }

        [XmlElement(ElementName = "ConsignmentSenderIsResidential")]
        public bool ConsignmentSenderIsResidential { get; set; }

        [XmlElement(ElementName = "ReceiverDetails")]
        public ReceiverDetails ReceiverDetails { get; set; }

        [XmlElement(ElementName = "PickupRequest")]
        public bool PickupRequest { get; set; }

        [XmlElement(ElementName = "ReceiverIsResidential")]
        public bool ReceiverIsResidential { get; set; }

        [XmlElement(ElementName = "ReturnPdfLabels")]
        public bool ReturnPdfLabels { get; set; }

        [XmlElement(ElementName = "ReturnPdfConsignment")]
        public bool ReturnPdfConsignment { get; set; }

        [XmlElement(ElementName = "SpecialInstructions")]
        public string SpecialInstructions { get; set; }

        [XmlElement(ElementName = "ConsignmentOtherReferences")]
        public string ConsignmentOtherReferences { get; set; }

        [XmlElement(ElementName = "ConsignmentOtherReferences2")]
        public string ConsignmentOtherReferences2 { get; set; }

        [XmlElement("Consignment")]
        public List<Row> Rows { get; set; } = new List<Row>();

        [XmlAnyElement]
        [JsonIgnore]
        public List<XmlElement> RowElements { get; set; }
    }
}