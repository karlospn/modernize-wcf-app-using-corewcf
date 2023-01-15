using System.Linq;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Domain.DomainServiceContracts
{
    public interface IBookingCreatorDomainServices
    {
        void CancelBooking(int bookingId);
        int CreateBooking(Booking booking);
        IQueryable<Booking> GetCanceled();
        IQueryable<Booking> GetActives();
    }
}
