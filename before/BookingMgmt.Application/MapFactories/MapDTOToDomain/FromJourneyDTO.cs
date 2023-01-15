using System;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Application.MapFactories.MapDTOToDomain
{
    internal class FromJourneyDTO : MappingBase
    {
        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is JourneyDTO dto)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new Journey
            {
                Arrival = dto.Arrival,
                ArrivalDate = dto.ArrivalDate,
                BookingId = dto.BookingId,
                Departure = dto.Departure,
                DepartureDate = dto.DepartureDate,
                Id = dto.Id,
                Price = dto.Price
            } as TOutput;
        }

    }
}
