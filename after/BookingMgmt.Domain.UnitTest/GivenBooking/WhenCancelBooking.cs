using System.Linq.Expressions;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.Exceptions;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.UnitTest.Mocks;
using BookingMgmt.Domain.Validations;
using BookingMgmt.SharedKernel.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenCancelBooking
    {
        private static IBookingCreatorDomainServices _sut;
        private static Mock<IUnitOfWorkBookingCreator> _unitOfWorkBookingCreator;
        private static Mock<IRepository<Booking>> _bookingRepository;
        private static Mock<IRepository<Journey>> _journeyRepository;
        private static Mock<IBookingFeaturesDomainServices> _bookingFeaturesDomainServices;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _unitOfWorkBookingCreator = new Mock<IUnitOfWorkBookingCreator>();

            _bookingRepository = new Mock<IRepository<Booking>>();

            _journeyRepository = new Mock<IRepository<Journey>>();

            _unitOfWorkBookingCreator.Setup(x => x.GetRepository<Booking>()).Returns(_bookingRepository.Object);

            _unitOfWorkBookingCreator.Setup(x => x.GetRepository<Journey>()).Returns(_journeyRepository.Object);

            _bookingFeaturesDomainServices = new Mock<IBookingFeaturesDomainServices>();
            
            var  iBookingCancelerValidations = new BookingCreatorValidations(_bookingFeaturesDomainServices.Object);
            
            _sut = new BookingCreatorDomainServices(_unitOfWorkBookingCreator.Object, iBookingCancelerValidations);

        }

        [TestMethod]
        public void Then_BookingIsCanceled()
        {

            var salesAgent = "web";
            var amountOfBooking = 1;

            var bookingsToCancel = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking);
            var firstModificationDate = bookingsToCancel.SingleOrDefault().Modified;
            var journeysAmountToCancel = bookingsToCancel.SingleOrDefault().Journeys.Count;

            _bookingRepository.Setup(x => x.Get(
                It.IsAny<Expression<Func<Booking, bool>>>(), 
                It.IsAny<Func<IQueryable<Booking>, IOrderedQueryable<Booking>>>(), 
                It.IsAny<List<Expression<Func<Booking, object>>>>(), 
                It.IsAny<int?>(), 
                It.IsAny<int?>(), 
                It.IsAny<bool>())).Returns(bookingsToCancel);

            _journeyRepository.Setup(x => x.Delete(It.IsAny<Journey>()));

            _bookingRepository.Setup(x => x.UpdateRootEntity(It.IsAny<Booking>()));

            _unitOfWorkBookingCreator.Setup(x => x.Save()).Returns(journeysAmountToCancel);

            _bookingFeaturesDomainServices.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(false);

            _sut.CancelBooking(bookingsToCancel.First().Id);

            Assert.IsTrue(bookingsToCancel.SingleOrDefault()?.Modified > firstModificationDate, "Property Modified in Booking was not modified.");

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_BookingIsNotCanceledWhenAgency()
        {

            var salesAgent = "agency";
            var amountOfBooking = 1;

            var bookingsToCancel = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking);
            var journeysAmountToCancel = bookingsToCancel.SingleOrDefault().Journeys.Count;

            _bookingRepository.Setup(x => x.Get(
                It.IsAny<Expression<Func<Booking, bool>>>(),
                It.IsAny<Func<IQueryable<Booking>, IOrderedQueryable<Booking>>>(),
                It.IsAny<List<Expression<Func<Booking, object>>>>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>())).Returns(bookingsToCancel);

            _journeyRepository.Setup(x => x.Delete(It.IsAny<Journey>()));

            _bookingRepository.Setup(x => x.UpdateRootEntity(It.IsAny<Booking>()));

            _unitOfWorkBookingCreator.Setup(x => x.Save()).Returns(journeysAmountToCancel);

            _bookingFeaturesDomainServices.Setup(x => x.IsAgency(It.IsAny<Booking>())).Returns(true);

            _sut.CancelBooking(bookingsToCancel.First().Id);
        }
    }
}
