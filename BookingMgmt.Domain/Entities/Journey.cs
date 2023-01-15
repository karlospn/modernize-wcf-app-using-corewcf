using System;

namespace BookingMgmt.Domain.Entities
{
    public class Journey
    {
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
