using System;
using System.Diagnostics;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using BookingMgmt.Application.Implementation;
using BookingMgmt.Contracts;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.Validations;
using BookingMgmt.Infrastructure.UnitOfWorks;
using BookingMgmt.SharedKernel.UnitOfWork;

namespace BookingMgmt.WCF.WebService
{
    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                RegisterDependencies();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                HttpRuntime.UnloadAppDomain(); // Make sure we try to run Application_Start again next request
                throw new AppStartException(ex);
            }
        }
        
        private static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<BookingCreatorApplicationServices>().As<IBookingCreatorApplicationServices>();
            builder.RegisterType<BookingCreatorDomainServices>().As<IBookingCreatorDomainServices>();
            builder.RegisterType<BookingCreatorService>().As<IBookingCreatorService>();
            builder.RegisterType<UnitOfWorkBookingCreator>().As<IUnitOfWorkBookingCreator>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<BookingCreatorValidations>().As<IBookingCreatorValidations>();
            builder.RegisterType<BookingFeaturesDomainServices>().As<IBookingFeaturesDomainServices>();

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastException = Server.GetLastError();
            if (lastException == null) return;
            Trace.TraceError(string.Format("Unhandled error. {0}", lastException));
            if (lastException.GetType() != typeof(AppStartException))
                Server.ClearError();
            else
                throw lastException;
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
    }


    [Serializable]
    public class AppStartException : Exception
    {
        public AppStartException() { }
        public AppStartException(string message) : base(message) { }
        public AppStartException(Exception inner) : base(inner.Message, inner) { }
        public AppStartException(string message, Exception inner) : base(message, inner) { }
        protected AppStartException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
