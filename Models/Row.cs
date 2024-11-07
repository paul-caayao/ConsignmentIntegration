using System.Xml.Serialization;

namespace ConsignmentIntegration.Models
{
    public class Row
    {
        [XmlElement("Reference")]
        public string Reference { get; set; }

        [XmlElement("Qty")]
        public int Qty { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("ItemContentsDescription")]
        public string ItemContentsDescription { get; set; }

        [XmlElement("Weight")]
        public double Weight { get; set; }

        [XmlElement("Width")]
        public double Width { get; set; }

        [XmlElement("Length")]
        public double Length { get; set; }

        [XmlElement("Height")]
        public double Height { get; set; }

        [XmlElement("DangerousGoodsUNNumber")]
        public string DangerousGoodsUNNumber { get; set; }

        [XmlElement("DangerousGoodsClass")]
        public string DangerousGoodsClass { get; set; }

        [XmlElement("DangerousGoodsSubRisk")]
        public string DangerousGoodsSubRisk { get; set; }

        [XmlElement("DangerousGoodsPackagingGroup")]
        public string DangerousGoodsPackagingGroup { get; set; }

        [XmlArray("Barcodes")]
        [XmlArrayItem("Barcode")]
        public List<string> Barcodes { get; set; } = new List<string>();
    }
}
