using BookingMgmt.Application.Implementation;
using BookingMgmt.Contracts;
using BookingMgmt.CoreWCF.WebService;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.Validations;
using BookingMgmt.Infrastructure.UnitOfWorks;
using BookingMgmt.SharedKernel.UnitOfWork;

var builder = WebApplication.CreateBuilder();

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

builder.Services.AddSingleton<BookingCreatorService>();
builder.Services.AddTransient<IBookingCreatorApplicationServices, BookingCreatorApplicationServices>();
builder.Services.AddTransient<IBookingCreatorDomainServices, BookingCreatorDomainServices>();
builder.Services.AddTransient<IBookingCreatorService, BookingCreatorService> ();
builder.Services.AddTransient<IUnitOfWorkBookingCreator, UnitOfWorkBookingCreator>();
builder.Services.AddTransient<IBookingCreatorValidations, BookingCreatorValidations>();
builder.Services.AddTransient<IBookingFeaturesDomainServices, BookingFeaturesDomainServices>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<BookingCreatorService>();
    serviceBuilder.AddServiceEndpoint<BookingCreatorService, IBookingCreatorService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/BookingCreatorService.svc");
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpsGetEnabled = true;
    serviceMetadataBehavior.HttpGetEnabled = true;
});

app.Run();
