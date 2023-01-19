using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Domain.UnitTest.Mocks
{
    internal  static class BookingMockedProvider
    {
        internal static IQueryable<Booking> GetBookings(string salesAgent, int amountOfBooking)
        {
            var bookings = new List<Booking>();
            for (var i = 0; i < amountOfBooking; i++)
            {
                bookings.Add(GetNewBooking(salesAgent, i + 1));
            }
            return bookings.AsQueryable();
        }

        internal static Booking GetNewBooking(string salesAgent, int addDays)
        {
            return new Booking
            {
                Id = 1 + addDays,
                SalesAgent = salesAgent,
                Journeys = new List<Journey>
                {
                    new Journey
                    {
                        Id = 1 + addDays,
                        Arrival = "MAD",
                        BookingId = 1 + addDays,
                        Departure = "BCN",
                        Price = 99,
                        ArrivalDate = DateTime.UtcNow.AddDays(addDays),
                        DepartureDate = DateTime.UtcNow.AddDays(addDays)
                    }
                },
                RecordLocator = "XXX000"
            };
        }
    }
}
