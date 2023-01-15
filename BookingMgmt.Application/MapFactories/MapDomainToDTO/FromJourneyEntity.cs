using System;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Application.MapFactories.MapDomainToDTO
{
    internal class FromJourneyEntity : MappingBase
    {
        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is Journey entity)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new JourneyDTO
            {
                Arrival = entity.Arrival,
                ArrivalDate = entity.ArrivalDate,
                BookingId = entity.BookingId,
                Departure = entity.Departure,
                DepartureDate = entity.DepartureDate,
                Id = entity.Id,
                Price = entity.Price

            } as TOutput;
        }

    }
}
