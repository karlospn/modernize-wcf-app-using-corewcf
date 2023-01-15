using System.Linq;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.Exceptions;

namespace BookingMgmt.Domain.Validations
{
    public class BookingCreatorValidations : IBookingCreatorValidations
    {
        private readonly IBookingFeaturesDomainServices _bookingFeaturesDomainServices;

        public BookingCreatorValidations(IBookingFeaturesDomainServices iBookingFeaturesDomainServices)
        {
            _bookingFeaturesDomainServices = iBookingFeaturesDomainServices;
        }

        public void Validate(Booking booking)
        {
            ValidateBookingNull(booking);
            ValidateJourneysNull(booking);
            ValidateBookingIsAlreadyFlew(booking);
            ValidateBookingIsAgency(booking);
        }

        private void ValidateBookingNull(Booking booking)
        {
            if (booking == null)
            {
                throw new InvalidBookingOperationException("Booking is null.");
            }
        }

        private void ValidateJourneysNull(Booking booking)
        {
            if (!booking.Journeys.Any())
            {
                throw new InvalidBookingOperationException("Journeys can't be null. Maybe it was canceled previously.");
            }
        }

        private void ValidateBookingIsAlreadyFlew(Booking booking)
        {
            if (booking.IsAlreadyFlew())
            {
                throw new InvalidBookingOperationException("Booking flew.");
            }
        }

        private void ValidateBookingIsAgency(Booking booking)
        {
            if (_bookingFeaturesDomainServices.IsAgency(booking))
            {
                throw new InvalidBookingOperationException("This booking was created by some Agency.");
            }
        }

    }
}
