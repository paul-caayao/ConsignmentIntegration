using AutoMapper;
using ConsignmentIntegration.Configuration;
using ConsignmentIntegration.Logic.DTO;
using ConsignmentIntegration.Logic.Interfaces;
using ConsignmentIntegration.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ConsignmentIntegration.Logic.Services
{
    public class TransVirtualService : ITransVirtualService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly TransVirtualSettings _transvirtualSettings;

        public TransVirtualService(HttpClient httpClient, IOptions<TransVirtualSettings> transvirtualSettings, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _transvirtualSettings = transvirtualSettings.Value;
        }

        public async Task<ConsignmentResponse> SendConsignmentToApi(Consignment consignment)
        {
            try
            {
                var consignmentRequest = _mapper.Map<ConsignmentRequest>(consignment);

                var jsonContent = JsonConvert.SerializeObject(consignmentRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", _transvirtualSettings.AuthorizationCode);

                var response = await _httpClient.PostAsync($"{_transvirtualSettings.ApiBaseUrl}/Consignment", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Consignment sent successfully!");

                    var responseContent = await response.Content.ReadAsStringAsync();

                    //Parse the Data to retrieve the response properties that includes PdfLabels and PdfConsignment
                    JObject jsonObject = JObject.Parse(responseContent);
                    JObject dataObject = (JObject)jsonObject["Data"];

                    ConsignmentResponse apiResponse = dataObject.ToObject<ConsignmentResponse>();
                    return apiResponse;
                }
                else
                {
                    Console.WriteLine($"API Response failed: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encountered an error sending Consignment: {ex.Message}");
                throw;
            }

        }

        public async Task ProcessPdfLabel(int id, string pdfLabels)
        {
            try
            {
                if (string.IsNullOrEmpty(pdfLabels))
                {
                    throw new ArgumentException("Pdf Label is missing", nameof(pdfLabels));
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{id}_{timestamp}.pdf"; // file name creation with timestamp to maintain uniqueness

                byte[] pdfBytes = Convert.FromBase64String(pdfLabels);
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string appFolderPath = Path.Combine(documentsPath, "TransVirtual Consignment Label");
                Directory.CreateDirectory(appFolderPath); // Make sure that the path is existing

                string outputPath = Path.Combine(appFolderPath, fileName);

                await File.WriteAllBytesAsync(outputPath, pdfBytes);
                Console.WriteLine($"PDF generated and saved at: {appFolderPath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate PDF: {ex.Message}");
            }
        }
    }
}
