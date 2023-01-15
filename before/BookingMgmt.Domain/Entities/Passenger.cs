namespace BookingMgmt.Domain.Entities
{
    public class Passenger
    {
        public Passenger()
        {
            PaxType = PassengerType.Unassigned;
        }

        public enum PassengerType
        {
            Unassigned = 0,
            INF = 1,
            CHD = 2,
            ADU = 3
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public PassengerType PaxType { get; set; }
        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
