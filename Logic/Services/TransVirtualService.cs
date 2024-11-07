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
               
                JObject jsonObject = JObject.Parse(responseContent);
                JObject dataObject = (JObject)jsonObject["Data"];

                ConsignmentResponse apiResponse = dataObject.ToObject<ConsignmentResponse>();
                return apiResponse;
            }
            else
            {
                Console.WriteLine($"Failed to send consignment: {response.StatusCode}");
                return null; 
            }
        }

        public async Task ProcessPdfLabel(int id, string pdfLabels)
        {
            if (string.IsNullOrEmpty(pdfLabels))
            {
                throw new ArgumentException("Pdf Label is missing", nameof(pdfLabels));
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"{id}_{timestamp}.pdf";

            byte[] pdfBytes = Convert.FromBase64String(pdfLabels);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appFolderPath = Path.Combine(documentsPath, "TransVirtual Consignment Label"); 
            Directory.CreateDirectory(appFolderPath);

            string outputPath = Path.Combine(appFolderPath, fileName);

            await File.WriteAllBytesAsync(outputPath, pdfBytes);
        }
    }
}
