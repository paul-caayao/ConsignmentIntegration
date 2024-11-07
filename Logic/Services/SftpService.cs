using ConsignmentIntegration.Configuration;
using ConsignmentIntegration.Logic.Interfaces;
using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace ConsignmentIntegration.Logic.Services
{
    public class SftpService : ISftpService
    {
        private readonly SftpSettings _sftpSettings;

        public SftpService(IOptions<SftpSettings> sftpSettings)
        {
            _sftpSettings = sftpSettings.Value;
        }
        async Task<string> ISftpService.RetrieveConsignment(string fileName)
        {
            try
            {
                using (var sftpClient = new SftpClient(_sftpSettings.Server, _sftpSettings.Username, _sftpSettings.Password))
                {
                    sftpClient.Connect();

                    using (var sftpStream = sftpClient.OpenRead(fileName))

                    using (var reader = new StreamReader(sftpStream))
                    {
                        string xmlContent = await reader.ReadToEndAsync();

                        return xmlContent;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured during the retrieval of XML file: {e.Message}");
                throw;

            }
        }
    }
}


