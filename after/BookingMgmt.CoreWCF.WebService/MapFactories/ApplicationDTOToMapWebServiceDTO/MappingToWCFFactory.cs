namespace BookingMgmt.CoreWCF.WebService.MapFactories.ApplicationDTOToMapWebServiceDTO
{
    internal static class MappingToWCFFactory
    {
        internal static MappingBase GetFor(DtoToWCFResponseEnum entityName)
        {
            switch (entityName)
            {
                case DtoToWCFResponseEnum.Booking:
                    return new BookingDtoToBookingResponse();
                default:
                    throw new NotImplementedException(
                        $"Missing mapping for {entityName.ToString()}.");
            }
        }
    }
}