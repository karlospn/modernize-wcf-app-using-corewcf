namespace BookingMgmt.CoreWCF.WebService.DTO
{
    [DataContract]
    public class BookingRequest
    {
        [DataMember]
        public string SalesAgent { get; set; }

        [DataMember]
        public IEnumerable<JourneyRequest> Journeys { get; set; }

    }
}