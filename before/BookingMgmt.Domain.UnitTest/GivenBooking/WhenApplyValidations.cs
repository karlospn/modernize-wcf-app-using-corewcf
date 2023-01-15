using System;
using System.Collections.Generic;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.Exceptions;
using BookingMgmt.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenApplyValidations
    {
        private static MockFactory _mockFactory;
        private static IBookingCreatorValidations _sut;
        private static Mock<IBookingFeaturesDomainServices> _bookingFeaturesDomainServicesMocked;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _mockFactory = new MockFactory();
            _bookingFeaturesDomainServicesMocked = _mockFactory.CreateMock<IBookingFeaturesDomainServices>();
            _sut = new BookingCreatorValidations(_bookingFeaturesDomainServicesMocked.MockObject);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenAgency()
        {
            _mockFactory.ClearExpectations();
            _bookingFeaturesDomainServicesMocked.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments().WillReturn(true);

            var booking = GetNewBooking("agency", 2);
            _sut.Validate(booking);

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenFlew()
        {
            _mockFactory.ClearExpectations();
            _bookingFeaturesDomainServicesMocked.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments().WillReturn(false);

            var booking = GetNewBooking("web", -2);
            _sut.Validate(booking);
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_ValidationFailWhenJourneysNull()
        {
            _mockFactory.ClearExpectations();
            _bookingFeaturesDomainServicesMocked.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments().WillReturn(false);
            var booking = GetNewBooking("web", 2);
            booking.Journeys.Clear();
            _sut.Validate(booking);
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void ValidationOKWhenMeetAllConditions()
        {
            _mockFactory.ClearExpectations();
            _bookingFeaturesDomainServicesMocked.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments().WillReturn(false);
            var booking = GetNewBooking("web", 2);
            _sut.Validate(booking);
            _mockFactory.VerifyAllExpectationsHaveBeenMet();
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
