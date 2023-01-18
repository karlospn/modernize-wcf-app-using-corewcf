using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Infrastructure.BoundedContexts;
using BookingMgmt.SharedKernel.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace BookingMgmt.Infrastructure.UnitOfWorks
{
    public class UnitOfWorkBookingCreator : UnitOfWorkBase, IUnitOfWorkBookingCreator
    {
        public UnitOfWorkBookingCreator(IConfiguration configuration)
            : base(new BookingCreatorContext(configuration))
        {
        }

    }
}
