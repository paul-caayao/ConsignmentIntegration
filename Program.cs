using ConsignmentIntegration.Configuration;
using ConsignmentIntegration.Logic;
using ConsignmentIntegration.Logic.Interfaces;
using ConsignmentIntegration.Logic.Services;
using ConsignmentIntegration.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.Configure<SftpSettings>(context.Configuration.GetSection("SftpSettings"));
    services.Configure<TransVirtualSettings>(context.Configuration.GetSection("TransVirtualSettings"));
    services.AddSingleton<ISftpService, SftpService>();
    services.AddSingleton<ITransVirtualService, TransVirtualService>();
    services.AddScoped<IXmlService, XmlService>();
    services.AddHttpClient<TransVirtualService>();
    services.AddAutoMapper(typeof(AutoMapperProfile));
});

var app = builder.Build();

var sftpService = app.Services.GetRequiredService<ISftpService>();
var xmlService = app.Services.GetRequiredService<IXmlService>();
var transVirtualService = app.Services.GetRequiredService<ITransVirtualService>();

Console.WriteLine("TransVirtual Technical Test - Paul Caayao");

//Hardcoded based on specification
string fileName = "Consignment.xml";

Console.WriteLine($"Initializing connection to FTP server via SFTP protocol and retrieving {fileName}");
string xmlContent = await sftpService.RetrieveConsignment(fileName);

if (xmlContent != null)
{
    Console.WriteLine("Starting deserialization of XML file...");
    var consignment = xmlService.DeserializeXml<Consignment>(xmlContent);
    Console.WriteLine("Processing additional items within the consignment...");
    xmlService.ProcessRows(consignment);

    Console.WriteLine("Creating new consignment into TransVirtual database... ");
    var response = await transVirtualService.SendConsignmentToApi(consignment);

    Console.WriteLine("Commencing PDF Label Generation... ");
    await transVirtualService.ProcessPdfLabel(response.Id, response.PdfLabels);
}

Console.WriteLine("Process complete.");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();




