using BookingMgmt.Application.Implementation;
using BookingMgmt.Application.UnitTest.Mocks;
using BookingMgmt.Contracts;
using BookingMgmt.Domain.DomainServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace BookingMgmt.Application.UnitTest.GivenBookingDto
{
    [TestClass]
    public class WhenCancelBooking
    {
        public static MockFactory _mockFactory;
        private static Mock<IBookingCreatorDomainServices> _bookingCreatorDomainServices;
        private static IBookingCreatorApplicationServices _sut;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _mockFactory = new MockFactory();
            _bookingCreatorDomainServices = _mockFactory.CreateMock<IBookingCreatorDomainServices>();

            _sut = new BookingCreatorApplicationServices(_bookingCreatorDomainServices.MockObject);
        }

        [TestMethod]
        public void Then_GetTotalPriceIsCalled()
        {
            _mockFactory.ClearExpectations();
            var expected = 225;
            var booking = BookingDtoMockProvider.GetNewBooking();
            var actual = _sut.GetTotalPrice(booking);

            Assert.IsTrue(actual == expected);

        }

        [TestMethod]
        public void Then_GetRouteIsCalled()
        {
            _mockFactory.ClearExpectations();
            var expected = "MADBIO";
            var booking = BookingDtoMockProvider.GetNewBooking();
            var actual = _sut.GetRoute(booking);

            Assert.IsTrue(actual == expected);

        }

    }
}
