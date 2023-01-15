using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;
using BookingMgmt.Domain.InfrastructureContracts;
using BookingMgmt.Domain.Validations;
using BookingMgmt.SharedKernel.UnitOfWork;

namespace BookingMgmt.Domain.DomainServicesImplementations
{
    public class BookingCreatorDomainServices : IBookingCreatorDomainServices
    {
        private const bool TrackingStateForEdit = true;

        private readonly IUnitOfWorkBookingCreator _unitOfWorkBookingCreator;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Journey> _journeyRepository;
        private readonly IBookingCreatorValidations _bookingCreatorValidations;

        public BookingCreatorDomainServices(
            IUnitOfWorkBookingCreator unitOfWorkBookingCreator,
            IBookingCreatorValidations bookingCreatorValidations)
        {
            _unitOfWorkBookingCreator = unitOfWorkBookingCreator;
            _bookingCreatorValidations = bookingCreatorValidations;

            _bookingRepository = _unitOfWorkBookingCreator.GetRepository<Booking>();
            _journeyRepository = _unitOfWorkBookingCreator.GetRepository<Journey>();
        }

        public void CancelBooking(int bookingId)
        {
            var booking = GetValidBooking(bookingId);

            _bookingCreatorValidations.Validate(booking);

            RemoveJourneys(booking);

            UpdateModificationDate(booking);

            _unitOfWorkBookingCreator.Save();
        }

        public int CreateBooking(Booking booking)
        {
            _bookingCreatorValidations.Validate(booking);
            booking.AddRecordLocator();
            booking.UpdateCreationDate();
            booking.UpdateModifiedDate();
            _bookingRepository.Insert(booking);
            _unitOfWorkBookingCreator.Save();
            return booking.Id;
        }


        public IQueryable<Booking> GetCanceled()
        {
            return _bookingRepository.Get(
                filter: x => !x.Journeys.Any(),
                orderBy: null,
                includeProperties: new List<Expression<Func<Booking, object>>>
                {
                    z => z.Journeys
                },
                page: null,
                pageSize: null
            );
        }

        public IQueryable<Booking> GetActives()
        {
            return _bookingRepository.Get(
                filter: x => x.Journeys.Any(),
                orderBy: null,
                includeProperties: new List<Expression<Func<Booking, object>>>
                {
                    z => z.Journeys
                },
                page: null,
                pageSize: null
            );
        }

        private Booking GetValidBooking(int bookingId)
        {
            return _bookingRepository.Get(
                    filter: x => x.Id == bookingId,
                    orderBy: null,
                    includeProperties:
                        new List<Expression<Func<Booking, object>>>
                            {
                                z => z.Journeys
                            },
                    page: null,
                    pageSize: null,
                    trackingEnabled: TrackingStateForEdit
                ).SingleOrDefault();
        }


        private void RemoveJourneys(Booking booking)
        {
            foreach (var journey in booking.Journeys.ToList())
            {
                _journeyRepository.Delete(journey);
            }
        }

        private void UpdateModificationDate(Booking booking)
        {
            booking.Modified = DateTime.UtcNow;

            _bookingRepository.UpdateRootEntity(booking);
        }

    }
}
