using System;
using System.Linq;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Application.MapFactories.MapDTOToDomain
{
    internal class FromBookingDTO : MappingBase
    {
        private readonly MappingBase jouneyMapping = 
            MappingToDomainFactory.GetFor(EnumDomain.Journey);

        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null) { return default; }

            if (!(source is BookingDTO dto)) { throw new InvalidCastException(typeof(TInput).Name); }

            return new Booking
            {
                Created = dto.Created,
                Id = dto.Id,
                Journeys = jouneyMapping.GetCollection<JourneyDTO, Journey>(dto.Journeys).ToList(),
                Modified = dto.Modified,
                RecordLocator = dto.RecordLocator,
                SalesAgent = dto.SalesAgent
            } as TOutput;
        }

    }
}
