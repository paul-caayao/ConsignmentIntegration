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

string fileName = "Consignment.xml";

string xmlContent = await sftpService.RetrieveConsignment(fileName);

if (xmlContent != null)
{
    var consignment = xmlService.DeserializeXml<Consignment>(xmlContent);
    xmlService.ProcessRows(consignment);

    var response = await transVirtualService.SendConsignmentToApi(consignment);
    await transVirtualService.ProcessPdfLabel(response.Id, response.PdfLabels);
}






