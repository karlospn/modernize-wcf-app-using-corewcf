using System;

namespace BookingMgmt.Application.MapFactories.MapDTOToDomain
{
    internal static class MappingToDomainFactory
    {
        internal static MappingBase GetFor(EnumDomain model)
        {
            switch (model)
            {
                case EnumDomain.Booking:
                    return new FromBookingDTO();
                case EnumDomain.Journey:
                    return new FromJourneyDTO();
                default:
                    throw new NotImplementedException($"The mapping for type {model} is not implemented.");
            }
        }
    }
}
