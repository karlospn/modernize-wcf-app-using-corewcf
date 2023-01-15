using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BookingMgmt.Contracts;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.WCF.WebService.DTO;
using BookingMgmt.WCF.WebService.MapFactories.ApplicationDTOToMapWebServiceDTO;
using BookingMgmt.WCF.WebService.MapFactories.MapWebServiceDTOToApplicationDTO;

namespace BookingMgmt.WCF.WebService
{
    public class BookingCreatorService : IBookingCreatorService
    {
        private readonly IBookingCreatorApplicationServices _bookingCreatorApplicationServices;

        public BookingCreatorService(IBookingCreatorApplicationServices bookingCreatorApplicationServices)
        {
            _bookingCreatorApplicationServices = bookingCreatorApplicationServices;
        }

        public int CreateBooking(BookingRequest booking)
        {
            try
            {
                ValidateRequest(booking);
                var mapping = MappingFromWCFRequestFactory.GetFor(WCFRequestToDtoEnum.Booking);
                var dto = mapping.Get<BookingRequest, BookingDTO>(booking);

                return _bookingCreatorApplicationServices.CreateBooking(dto);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public void CancelBooking(int bookingId)
        {
            try
            {
                _bookingCreatorApplicationServices.CancelBooking(bookingId);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            
        }

        public List<BookingResponse> GetCanceledBookings(int page, int pageSize)
        {
            try
            {
                
                var bookings = _bookingCreatorApplicationServices.GetCanceledByPages(page, pageSize);
                
                var response = new List<BookingResponse>();
                var mapping = MappingToWCFFactory.GetFor(DtoToWCFResponseEnum.Booking);

                foreach (var booking in bookings)
                {
                    var mappedBooking = mapping.Get<BookingDTO, BookingResponse>(booking);
                    if (mappedBooking != null)
                    {
                        response.Add(mappedBooking);
                    }
                }

                Trace.TraceInformation($"Retrieved {response.Count} bookings canceled.");
                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public List<BookingResponse> GetActiveBookings(int page, int pageSize)
        {
            try
            {
                var bookings = _bookingCreatorApplicationServices.GetActivesByPages(page, pageSize);

                var response = new List<BookingResponse>();
                var mapping = MappingToWCFFactory.GetFor(DtoToWCFResponseEnum.Booking);

                foreach (var booking in bookings)
                {
                    var mappedBooking = mapping.Get<BookingDTO, BookingResponse>(booking);
                    if (mappedBooking != null)
                    {
                        SetPriceAndRoute(mappedBooking, booking);
                        response.Add(mappedBooking);
                    }
                }

                Trace.TraceInformation($"Retrieved {response.Count} bookings canceled.");
                return response;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }


        private void ValidateRequest(BookingRequest booking)
        {
            if (booking.SalesAgent == null) throw new ArgumentNullException(nameof(booking.SalesAgent), "SalesAgent not set");
            if (booking.Journeys == null ||  booking.Journeys?.Count() == 0) throw new ArgumentNullException(nameof(booking.Journeys), "Journeys not set");
            foreach (var journey in booking.Journeys)
            {
                if (journey.Arrival == null) throw new ArgumentNullException(nameof(journey.Arrival), "Arrival not set");
                if (journey.Departure == null) throw new ArgumentNullException(nameof(journey.DepartureDate), "Departure not set");
            }
        }


        private void SetPriceAndRoute(BookingResponse bookingResponse, BookingDTO bookingDto)
        {
            bookingResponse.Route = _bookingCreatorApplicationServices.GetRoute(bookingDto);
            bookingResponse.TotalPrice = _bookingCreatorApplicationServices.GetTotalPrice(bookingDto);
        }
    }
}
