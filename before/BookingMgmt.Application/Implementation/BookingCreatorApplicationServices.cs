using System.Collections.Generic;
using System.Linq;
using BookingMgmt.Application.MapFactories.MapDomainToDTO;
using BookingMgmt.Application.MapFactories.MapDTOToDomain;
using BookingMgmt.Contracts;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.Domain.DomainServiceContracts;
using BookingMgmt.Domain.Entities;

namespace BookingMgmt.Application.Implementation
{
    public class BookingCreatorApplicationServices : IBookingCreatorApplicationServices
    {
        private readonly IBookingCreatorDomainServices _bookingCreatorDomainServices;

        public BookingCreatorApplicationServices(
            IBookingCreatorDomainServices bookingCreatorDomainServices)
        {
            _bookingCreatorDomainServices = bookingCreatorDomainServices;
        }

        public void CancelBooking(int bookingId)
        {
            _bookingCreatorDomainServices.CancelBooking(bookingId);
        }

        public int CreateBooking(BookingDTO bookingDto)
        {
            var booking = GetBookingDtoToBooking(bookingDto);
            return _bookingCreatorDomainServices.CreateBooking(booking);
        }

        public List<BookingDTO> GetCanceledByPages(int page, int pageSize)
        {
            var bookings = _bookingCreatorDomainServices.GetCanceled()
                .OrderBy(x => x.Id)
                .Skip(page - 1)
                .Take(page * pageSize)
                .ToList();

            return GetBookingToBookingDto(bookings);
        }


        public List<BookingDTO> GetActivesByPages(int page, int pageSize)
        {
            var bookings = _bookingCreatorDomainServices.GetActives()
                .OrderBy(x => x.Id)
                .Skip(page - 1)
                .Take(page * pageSize)
                .ToList();

            return GetBookingToBookingDto(bookings);
        }

        public decimal GetTotalPrice(BookingDTO bookingDto)
        {
            var booking = GetBookingDtoToBooking(bookingDto);

            return booking.GetTotalPrice();
        }

        public string GetRoute(BookingDTO bookingDto)
        {
            var booking = GetBookingDtoToBooking(bookingDto);

            return booking.GetRoute();
        }

        private Booking GetBookingDtoToBooking(BookingDTO bookingDto)
        {
            var bookingMapper = MappingToDomainFactory.GetFor(EnumDomain.Booking);
            return bookingMapper.Get<BookingDTO,Booking>(bookingDto);
        }

        private List<BookingDTO> GetBookingToBookingDto(List<Booking> entities)
        {
            var mapping = MappingFromDomainFactory.GetFor(DomainToDtoEnum.Booking);
            return mapping.GetCollection<Booking, BookingDTO>(entities).ToList();
        }
    }
}
