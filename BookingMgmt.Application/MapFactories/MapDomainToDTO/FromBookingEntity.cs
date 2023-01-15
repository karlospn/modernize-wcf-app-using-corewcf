using System;
using System.Linq;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Application.MapFactories.MapDomainToDTO
{
    internal class FromBookingEntity : MappingBase
    {
        private readonly MappingBase jouneyMapping = 
            MappingFromDomainFactory.GetFor(DomainToDtoEnum.Journey);

        internal override TOutput Get<TInput, TOutput>(TInput source)
        {
            if (source == null)
            {
                return default;
            }

            if (!(source is Booking entity))
            {
                throw new InvalidCastException(typeof(TInput).Name);
            }

            return new BookingDTO
            {
                Created = entity.Created,
                Id = entity.Id,
                Journeys = jouneyMapping
                    .GetCollection<Journey, JourneyDTO>(entity.Journeys)
                    .ToList(),
                Modified = entity.Modified,
                RecordLocator = entity.RecordLocator,
                SalesAgent = entity.SalesAgent,
                TotalPrice = entity.GetTotalPrice()
            } as TOutput;
        }
    }
}
