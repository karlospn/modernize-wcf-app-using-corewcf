using System;

namespace BookingMgmt.WCF.WebService.MapFactories.MapWebServiceDTOToApplicationDTO
{
    internal static class MappingFromWCFRequestFactory
    {
        internal static MappingBase GetFor(WCFRequestToDtoEnum model)
        {
            switch (model)
            {
                case WCFRequestToDtoEnum.Booking:
                    return new BookingRequestToDto();
                case WCFRequestToDtoEnum.Journey:
                    return new JourneyRequestToDto();
                default:
                    throw new NotImplementedException(
                        $"Missing mapping for {model.ToString()}.");
            }
        }
    }
}