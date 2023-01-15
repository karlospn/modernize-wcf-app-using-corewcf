using System.Runtime.Serialization;

namespace BookingMgmt.WCF.WebService.DTO
{
    [DataContract]
    public class BookingResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SalesAgent { get; set; }

        [DataMember]
        public System.DateTime Created { get; set; }

        [DataMember]
        public System.DateTime Modified { get; set; }

        [DataMember]
        public string RecordLocator { get; set; }

        [DataMember]
        public string Route { get; set; }

        [DataMember]
        public int TotalJourneys { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }

    }
}