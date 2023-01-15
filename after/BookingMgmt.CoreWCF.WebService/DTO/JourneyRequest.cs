namespace BookingMgmt.CoreWCF.WebService.DTO
{
    [DataContract]
    public class JourneyRequest
    {
        [DataMember]
        public string Departure { get; set; }

        [DataMember]
        public string Arrival { get; set; }

        [DataMember]
        public DateTime DepartureDate { get; set; }

        [DataMember]
        public DateTime ArrivalDate { get; set; }

        [DataMember]
        public decimal Price { get; set; }

    }
}