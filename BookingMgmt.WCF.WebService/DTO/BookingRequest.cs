using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookingMgmt.WCF.WebService.DTO
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