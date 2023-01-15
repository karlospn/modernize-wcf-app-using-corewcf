namespace BookingMgmt.Domain.DomainServiceContracts
{
    public interface IBookingFeaturesDomainServices
    {
        bool IsAgency(Entities.Booking booking);
        bool IsCorporate(Entities.Booking booking);
        bool IsEnabledToAddNewJourneys(Entities.Booking booking);
    }
}