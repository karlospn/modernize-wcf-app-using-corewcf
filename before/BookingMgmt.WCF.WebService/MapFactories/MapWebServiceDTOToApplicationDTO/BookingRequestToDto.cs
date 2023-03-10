using System;
using System.Linq;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.WCF.WebService.DTO;

namespace BookingMgmt.WCF.WebService.MapFactories.MapWebServiceDTOToApplicationDTO
{
    public class BookingRequestToDto : MappingBase
    {
        private readonly MappingBase jouneyMapping =
            MappingFromWCFRequestFactory.GetFor(WCFRequestToDtoEnum.Journey);

        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is BookingRequest dto)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new BookingDTO()
            {
                SalesAgent = dto.SalesAgent,
                Journeys = jouneyMapping
                    .GetCollection<JourneyRequest, JourneyDTO>(dto.Journeys)
                    .ToList(),
            } as TOutput;
        }
    }
}