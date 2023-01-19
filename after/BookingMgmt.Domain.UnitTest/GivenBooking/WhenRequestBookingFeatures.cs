using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenRequestBookingFeatures
    {
        private static IBookingFeaturesDomainServices _sut;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _sut = new BookingFeaturesDomainServices();
        }


        [TestMethod]
        public void Then_IsAgencyPassWithAgencyAgent()
        {
            var expected = true;
            var salesAgent = "Agency";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();


            var actual = _sut.IsAgency(booking);
            Assert.IsTrue(actual == expected, "Expected to pass IsAgency feature.");
        }

        [TestMethod]
        public void Then_IsAgencyFailWithWebAgent()
        {
            var expected = false;
            var salesAgent = "web";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            var actual = _sut.IsAgency(booking);
            Assert.IsTrue(actual == expected, "Expected to fail IsAgency feature.");
        }

        [TestMethod]
        public void Then_IsCorporatePassWithCorporateAgent()
        {
            var expected = true;
            var salesAgent = "Corporate";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            bool actual = _sut.IsCorporate(booking);
            Assert.IsTrue(actual == expected, "Expected to pass IsCorporate feature.");
        }

        [TestMethod]
        public void Then_IsCorporateFailWithWebAgent()
        {
            var expected = false;
            var salesAgent = "web";
            var amountOfBooking = 1;
            var booking = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking).First();

            var actual = _sut.IsCorporate(booking);
            Assert.IsTrue(actual == expected, "Expected to fail IsCorporate feature.");
        }

    }
}
