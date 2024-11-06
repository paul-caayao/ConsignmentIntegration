using ConsignmentIntegration.Models;
using Microsoft.Extensions.Configuration;
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var ftpSettings = configuration.GetSection("FtpSettings").Get<FtpSettings>();

Console.WriteLine("Consignment Integration - Transvirtual Technical Test by Paul Caayao");
Console.WriteLine($"Connecting to FTP Server {ftpSettings.Server}:{ftpSettings.Port} to access XML file...");

