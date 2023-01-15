using System.Linq;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.DomainServicesImplementations;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.Exceptions;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.UnitTest.Mocks;
using BookingMgmt.Domain.Validations;
using BookingMgmt.SharedKernel.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace BookingMgmt.Domain.UnitTest.GivenBooking
{
    [TestClass]
    public class WhenCancelBooking
    {
        private static MockFactory _mockFactory;
        private static IBookingCreatorDomainServices _sut;
        private static Mock<IUnitOfWorkBookingCreator> _unitOfWorkBookingCreator;
        private static Mock<IRepository<Booking>> _bookingRepository;
        private static Mock<IRepository<Journey>> _journeyRepository;
        private static Mock<IBookingFeaturesDomainServices> _bookingFeaturesDomainServices;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _mockFactory = new MockFactory();

            _unitOfWorkBookingCreator = _mockFactory.CreateMock<IUnitOfWorkBookingCreator>();

            _bookingRepository = _mockFactory.CreateMock<IRepository<Booking>>();

            _journeyRepository = _mockFactory.CreateMock<IRepository<Journey>>();

            _unitOfWorkBookingCreator.Expects.One.Method(x =>
                    x.GetRepository<Booking>()).WillReturn(_bookingRepository.MockObject);

            _unitOfWorkBookingCreator.Expects.One.Method(x => 
                x.GetRepository<Journey>()).WillReturn(_journeyRepository.MockObject);

            _bookingFeaturesDomainServices = _mockFactory.CreateMock<IBookingFeaturesDomainServices>();
            
            var  iBookingCancelerValidations = new BookingCreatorValidations(_bookingFeaturesDomainServices.MockObject);
            _sut = new BookingCreatorDomainServices(_unitOfWorkBookingCreator.MockObject, iBookingCancelerValidations);

        }

        [TestMethod]
        public void Then_BookingIsCanceled()
        {
            _mockFactory.ClearExpectations();

            var salesAgent = "web";
            var amountOfBooking = 1;

            var bookingsToCancel = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking);
            var firstModificationDate = bookingsToCancel.SingleOrDefault().Modified;
            var journeysAmountToCancel = bookingsToCancel.SingleOrDefault().Journeys.Count;

            _bookingRepository.Expects.One.Method(x => x.Get(null, null, null, null, null, false))
                .WithAnyArguments()
                .WillReturn(bookingsToCancel);

            _journeyRepository.Expects.Exactly(journeysAmountToCancel).Method(x => x.Delete(null))
                .WithAnyArguments();

            _bookingRepository.Expects.One.Method(x => x.UpdateRootEntity(null))
                .WithAnyArguments();

            _unitOfWorkBookingCreator.Expects.One.Method(x => x.Save())
                .WillReturn(journeysAmountToCancel);

            _bookingFeaturesDomainServices.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments()
                .WillReturn(false);

            _sut.CancelBooking(bookingsToCancel.First().Id);

            Assert.IsTrue(bookingsToCancel.SingleOrDefault()?.Modified > firstModificationDate, "Property Modified in Booking was not modified.");

            _mockFactory.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidBookingOperationException))]
        public void Then_BookingIsNotCanceledWhenAgency()
        {
            _mockFactory.ClearExpectations();

            var salesAgent = "agency";
            var amountOfBooking = 1;

            var bookingsToCancel = BookingMockedProvider.GetBookings(salesAgent, amountOfBooking);
            var journeysAmountToCancel = bookingsToCancel.SingleOrDefault().Journeys.Count;

            _bookingRepository.Expects.One.Method(x => 
                x.Get(null, null, null, null, null, false))
                .WithAnyArguments()
                .WillReturn(bookingsToCancel);

            _journeyRepository.Expects.Exactly(journeysAmountToCancel).Method(x => x.Delete(null))
                .WithAnyArguments();

            _bookingRepository.Expects.One.Method(x => x.UpdateRootEntity(null))
                .WithAnyArguments();

            _unitOfWorkBookingCreator.Expects.One.Method(x => x.Save())
                .WillReturn(journeysAmountToCancel);

            _bookingFeaturesDomainServices.Expects.One.Method(x => x.IsAgency(null))
                .WithAnyArguments()
                .WillReturn(true);

            _sut.CancelBooking(bookingsToCancel.First().Id);
        }
    }
}
