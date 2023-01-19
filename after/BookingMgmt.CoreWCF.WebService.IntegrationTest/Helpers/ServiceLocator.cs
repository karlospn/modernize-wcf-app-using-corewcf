using BookingMgmt.Application.Implementation;
using BookingMgmt.Contracts;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.Validations;
using BookingMgmt.Infrastructure.UnitOfWorks;
using BookingMgmt.SharedKernel.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingMgmt.CoreWCF.WebService.IntegrationTest.Helpers
{
    public static class ServiceLocator
    {
        private static ServiceProvider _container;

        public static void Build()
        {
            var builder = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .Build();
            builder.AddSingleton<IConfiguration>(configuration);

            builder.AddTransient<IBookingCreatorApplicationServices, BookingCreatorApplicationServices>();
            builder.AddTransient<IBookingCreatorDomainServices, BookingCreatorDomainServices>();
            builder.AddTransient<IBookingCreatorService, BookingCreatorService>();
            builder.AddTransient<IUnitOfWorkBookingCreator, UnitOfWorkBookingCreator>();
            builder.AddTransient<IBookingCreatorValidations, BookingCreatorValidations>();
            builder.AddTransient<IBookingFeaturesDomainServices, BookingFeaturesDomainServices>();
            builder.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            _container = builder.BuildServiceProvider();
        }

        internal static T Resolve<T>()
        {
            return _container.GetRequiredService<T>();
        }
    }
}
