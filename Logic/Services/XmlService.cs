using ConsignmentIntegration.Logic.Interfaces;
using ConsignmentIntegration.Models;
using System.Xml.Serialization;

namespace ConsignmentIntegration.Logic.Services
{
    public class XmlService : IXmlService
    {
        public T DeserializeXml<T>(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlContent))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public void ProcessRows(Consignment consignment)
        {
            // Group the rows by number to represent individual item
            var rowGroups = consignment.RowElements
                .Where(e => e.Name.StartsWith("Row_")) 
                .GroupBy(e => e.Name.Split('_')[1])    
                .ToList();

            foreach (var group in rowGroups)
            {
                var row = new Row();
                var barcodeProperties = group.Where(e => e.Name.StartsWith($"Row_{group.Key}_Barcode"));

                foreach (var element in group)
                {
                    var propertyName = element.Name.Split('_')[2]; 
                    var property = typeof(Row).GetProperty(propertyName);

                    if (property != null)
                    {
                        object value = Convert.ChangeType(element.InnerText, property.PropertyType);
                        property.SetValue(row, value);
                    }
                }

                foreach (var barcodeElement in barcodeProperties)
                {
                    row.Barcodes.Add(barcodeElement.InnerText); 
                }

                consignment.Rows.Add(row);
            }
        }
    }
}
