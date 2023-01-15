using System.Linq;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenRequestBookingFeatures
    {
        private static MockFactory _mockFactory;
        private static IBookingFeaturesDomainServices _sut;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _mockFactory = new MockFactory();
            _sut = new BookingFeaturesDomainServices();
        }


        [TestMethod]
        public void Then_IsAgencyPassWithAgencyAgent()
        {
            _mockFactory.ClearExpectations();
            
            var expected = true;
            var salesAgent = "Agency";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();


            var actual = _sut.IsAgency(booking);
            Assert.IsTrue(actual == expected, "Expected to pass IsAgency feature.");

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void Then_IsAgencyFailWithWebAgent()
        {
            _mockFactory.ClearExpectations();

            var expected = false;
            var salesAgent = "web";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            var actual = _sut.IsAgency(booking);
            Assert.IsTrue(actual == expected, "Expected to fail IsAgency feature.");

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void Then_IsCorporatePassWithCorporateAgent()
        {
            _mockFactory.ClearExpectations();

            var expected = true;
            var salesAgent = "Corporate";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            bool actual = _sut.IsCorporate(booking);
            Assert.IsTrue(actual == expected, "Expected to pass IsCorporate feature.");

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void Then_IsCorporateFailWithWebAgent()
        {
            _mockFactory.ClearExpectations();

            var expected = false;
            var salesAgent = "web";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            var actual = _sut.IsCorporate(booking);
            Assert.IsTrue(actual == expected, "Expected to fail IsCorporate feature.");

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }

    }
}
