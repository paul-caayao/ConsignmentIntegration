using ConsignmentIntegration.Logic.DTO;
using ConsignmentIntegration.Models;

namespace ConsignmentIntegration.Logic.Interfaces
{
    public interface ITransVirtualService
    {
        Task<ConsignmentResponse> SendConsignmentToApi(Consignment consignment);

        Task ProcessPdfLabel(int id, string pdfLabel);
    }
}
