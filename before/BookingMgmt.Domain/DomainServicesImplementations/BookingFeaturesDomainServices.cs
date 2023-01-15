using System;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Domain.DomainServicesImplementations
{
    public class BookingFeaturesDomainServices : IBookingFeaturesDomainServices
    {

        public bool IsEnabledToAddNewJourneys(Booking booking)
        {
            return booking.Journeys == null || 
                   booking.Journeys.Count < 15;
        }

        public bool IsAgency(Booking booking)
        {
            return !string.IsNullOrEmpty(booking.SalesAgent) && 
                   booking.SalesAgent.Equals("Agency", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsCorporate(Booking booking)
        {
            return !string.IsNullOrEmpty(booking.SalesAgent) && 
                   booking.SalesAgent.Equals("Corporate", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
