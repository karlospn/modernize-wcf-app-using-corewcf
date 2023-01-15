using System.Collections.Generic;

namespace BookingMgmt.Contracts.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string SalesAgent { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public string RecordLocator { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<JourneyDTO> Journeys { get; set; }
    }
}
