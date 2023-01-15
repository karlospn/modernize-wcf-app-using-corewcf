using Autofac;
using BookingMgmt.Application.Implementation;
using BookingMgmt.Contracts;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.Validations;
using BookingMgmt.Infrastructure.UnitOfWorks;
using BookingMgmt.SharedKernel.UnitOfWork;

namespace BookingMgmt.WCF.WebService.IntegrationTest.Helpers
{
    public static class ServiceLocator
    {
        private static IContainer _container;

        public static void Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BookingCreatorApplicationServices>().As<IBookingCreatorApplicationServices>();
            builder.RegisterType<BookingCreatorDomainServices>().As<IBookingCreatorDomainServices>();
            builder.RegisterType<BookingCreatorService>().As<IBookingCreatorService>();
            builder.RegisterType<UnitOfWorkBookingCreator>().As<IUnitOfWorkBookingCreator>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<BookingCreatorValidations>().As<IBookingCreatorValidations>();
            builder.RegisterType<BookingFeaturesDomainServices>().As<IBookingFeaturesDomainServices>();

            _container = builder.Build();
        }

        internal static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
