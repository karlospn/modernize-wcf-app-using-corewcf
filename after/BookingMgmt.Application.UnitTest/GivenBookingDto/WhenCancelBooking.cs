using BookingMgmt.Application.Implementation;
using BookingMgmt.Application.UnitTest.Mocks;
using BookingMgmt.Contracts;
using BookingMgmt.Domain.DomainServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingMgmt.Application.UnitTest.GivenBookingDto
{
    [TestClass]
    public class WhenCancelBooking
    {
       
        private static Mock<IBookingCreatorDomainServices> _bookingCreatorDomainServices;
        private static IBookingCreatorApplicationServices _sut;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _bookingCreatorDomainServices = new Mock<IBookingCreatorDomainServices>();
            _sut = new BookingCreatorApplicationServices(_bookingCreatorDomainServices.Object);
        }

        [TestMethod]
        public void Then_GetTotalPriceIsCalled()
        {
            var expected = 225;
            var booking = BookingDtoMockProvider.GetNewBooking();
            var actual = _sut.GetTotalPrice(booking);

            Assert.IsTrue(actual == expected);

        }

        [TestMethod]
        public void Then_GetRouteIsCalled()
        {
            var expected = "MADBIO";
            var booking = BookingDtoMockProvider.GetNewBooking();
            var actual = _sut.GetRoute(booking);

            Assert.IsTrue(actual == expected);

        }

    }
}
