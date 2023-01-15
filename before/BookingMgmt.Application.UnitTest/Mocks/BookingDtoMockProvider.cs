using System.Collections.Generic;
using BookingMgmt.Contracts.DTO;

namespace BookingMgmt.Application.UnitTest.Mocks
{
    internal static class BookingDtoMockProvider
    {
        internal static BookingDTO GetNewBooking()
        {
            return new BookingDTO
            {
                Journeys = new List<JourneyDTO>
                {
                    new JourneyDTO
                    {
                        Departure = "MAD",
                        Arrival = "BCN",
                        Price = 150
                    },
                    new JourneyDTO
                    {
                        Departure = "BCN",
                        Arrival = "BIO",
                        Price = 75
                    }
                }
            };
        }
    }
}
