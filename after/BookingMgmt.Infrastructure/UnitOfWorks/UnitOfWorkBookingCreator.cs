using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Infrastructure.BoundedContexts;
using BookingMgmt.SharedKernel.UnitOfWork;

namespace BookingMgmt.Infrastructure.UnitOfWorks
{
    public class UnitOfWorkBookingCreator : UnitOfWorkBase, IUnitOfWorkBookingCreator
    {
        public UnitOfWorkBookingCreator()
            : base(new BookingCreatorContext())
        {

        }

    }
}
