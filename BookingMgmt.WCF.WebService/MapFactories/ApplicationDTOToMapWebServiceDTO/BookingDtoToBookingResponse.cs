using System;
using System.Collections.Generic;
using System.Linq;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.WCF.WebService.DTO;

namespace BookingMgmt.WCF.WebService.MapFactories.ApplicationDTOToMapWebServiceDTO
{
    internal class BookingDtoToBookingResponse : MappingBase
    {
        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is BookingDTO dto)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new BookingResponse
            {
                Created = dto.Created,
                Id = dto.Id,
                Modified = dto.Modified,
                RecordLocator = dto.RecordLocator,
                SalesAgent = dto.SalesAgent,
                TotalJourneys = GetJourneysCount(dto.Journeys),
            } as TOutput;
        }



        private int GetJourneysCount(IEnumerable<JourneyDTO> journeys)
        {
            return journeys.Count();
        }

    }
}