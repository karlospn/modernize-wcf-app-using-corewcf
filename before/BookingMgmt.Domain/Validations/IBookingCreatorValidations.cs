using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Domain.Validations
{
    public interface IBookingCreatorValidations
    {
        void Validate(Booking booking);
    }
}
