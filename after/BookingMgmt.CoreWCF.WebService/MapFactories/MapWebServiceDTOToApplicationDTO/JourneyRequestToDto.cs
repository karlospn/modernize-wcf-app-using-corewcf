using BookingMgmt.Contracts.DTO;
using BookingMgmt.CoreWCF.WebService.DTO;

namespace BookingMgmt.CoreWCF.WebService.MapFactories.MapWebServiceDTOToApplicationDTO
{
    public class JourneyRequestToDto : MappingBase
    {
        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is JourneyRequest dto)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new JourneyDTO
            {
               Price = dto.Price,
               Departure = dto.Departure,
               Arrival = dto.Arrival,
               DepartureDate = dto.DepartureDate,
               ArrivalDate = dto.ArrivalDate
            } as TOutput;
        }
    }
}