using System;

namespace BookingMgmt.Application.MapFactories.MapDomainToDTO
{
    internal static class MappingFromDomainFactory
    {
        internal static MappingBase GetFor(DomainToDtoEnum entityName)
        {
            switch (entityName)
            {
                case DomainToDtoEnum.Booking:
                    return new FromBookingEntity();
                case DomainToDtoEnum.Journey:
                    return new FromJourneyEntity();
                default:
                    throw new NotImplementedException(
                        $"Missing mapping for {entityName.ToString()} in BookingMgmt.Application.MapFactories.MapDomainToDTO.");
            }
        }
    }
}
