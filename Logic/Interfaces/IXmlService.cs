using ConsignmentIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentIntegration.Logic.Interfaces
{
    public interface IXmlService
    {
        T DeserializeXml<T>(string xmlContent);

        void ProcessRows(Consignment consignment);
    }
}
