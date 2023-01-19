using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.Exceptions;
using BookingMgmt.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenApplyValidations
    {
        private static IBookingCreatorValidations _sut;
        private static Mock<IBookingFeaturesDomainServices> _bookingFeaturesDomainServicesMocked;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _bookingFeaturesDomainServicesMocked = new Mock<IBookingFeaturesDomainServices>();
            _sut = new BookingCreatorValidations(_bookingFeaturesDomainServicesMocked.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenAgency()
        {
            _bookingFeaturesDomainServicesMocked.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(true);
            var booking = GetNewBooking("agency", 2);
            _sut.Validate(booking);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenFlew()
        {

            _bookingFeaturesDomainServicesMocked.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(false);
            var booking = GetNewBooking("web", -2);
            _sut.Validate(booking);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenJourneysNull()
        {
            _bookingFeaturesDomainServicesMocked.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(false);
            var booking = GetNewBooking("web", 2);
            booking.Journeys.Clear();
            _sut.Validate(booking);
        }

        [TestMethod]
        public void ValidationOKWhenMeetAllConditions()
        {
            _bookingFeaturesDomainServicesMocked.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(false);
            var booking = GetNewBooking("web", 2);
            _sut.Validate(booking);
        }

        private Booking GetNewBooking(string salesAgent, int addDays)
        {
            return new Booking
            {
                Id = 1,
                SalesAgent = salesAgent,
                Journeys = new List<Journey>
                {
                    new Journey
                    {
                        ArrivalDate = DateTime.Now.AddDays(addDays),
                        DepartureDate = DateTime.Now.AddDays(addDays)
                    }
                }
            };
        }

    }
}
