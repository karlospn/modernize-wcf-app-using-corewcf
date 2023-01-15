using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingMgmt.Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            Created = DateTime.UtcNow;
            UpdateModifiedDate();
        }

        public int Id { get; set; }
        public string SalesAgent { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        string _recordLocator;
        public string RecordLocator
        {
            get => _recordLocator;
            set
            {
                UpdateModifiedDate();
                _recordLocator = value;
            }
        }

        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }

        public string GetRoute()
        {
            if (Journeys == null || Journeys.Count == 0) { return string.Empty; }

            var firstJourney = Journeys.OrderBy(x => x.DepartureDate).First();
            var lastJourney = Journeys.OrderBy(x => x.DepartureDate).Last();

            return $"{firstJourney.Departure}{lastJourney.Arrival}";
        }

        public void AddJourney(Journey journey)
        {
            ValidateNewJourney(journey);

            if (Journeys == null) { Journeys = new HashSet<Journey>(); }

            Journeys.Add(journey);

            UpdateModifiedDate();
        }

        public void AddPassenger(Passenger passenger)
        {
            ValidateNewPassenger(passenger);

            if (Passengers == null) { Passengers = new HashSet<Passenger>(); }

            EnsurePaxType(passenger);

            Passengers.Add(passenger);

            UpdateModifiedDate();
        }

        public void AddRecordLocator()
        {
            RecordLocator = Guid.NewGuid().ToString();
        }

        public decimal GetTotalPrice()
        {
            return !Journeys.Any() ? default : Journeys.Sum(x => x.Price);
        }

        public bool IsAlreadyFlew()
        {
            return Journeys != null && !Journeys.Any(x => x.DepartureDate > DateTime.UtcNow);
        }

        private static void EnsurePaxType(Passenger passenger)
        {
            if (passenger.PaxType == Passenger.PassengerType.Unassigned) { passenger.PaxType = Passenger.PassengerType.ADU; }
        }

        private void ValidateNewJourney(Journey journey)
        {
            if (journey == null) { throw new ArgumentNullException("journey"); }
            if (string.IsNullOrEmpty(journey.Departure) || string.IsNullOrEmpty(journey.Arrival)) { throw new ArgumentException("Paremeters Departure and Arrival are required to add new journey."); }
        }

        private void ValidateNewPassenger(Passenger passenger)
        {
            if (passenger == null) { throw new ArgumentNullException("passenger"); }
            if (string.IsNullOrEmpty(passenger.FullName)) { throw new ArgumentException("Paremeter FullName is required to add new passenger."); }
        }

        public void UpdateModifiedDate()
        {
            Modified = DateTime.UtcNow;
        }

        public void UpdateCreationDate()
        {
            Created = DateTime.UtcNow;
        }

    }
}
