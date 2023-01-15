using System.Collections.Generic;
using BookingMgmt.Contracts.DTO;

namespace BookingMgmt.Contracts
{
    public interface IBookingCreatorApplicationServices
    {
        void CancelBooking(int bookingId);
        List<BookingDTO> GetCanceledByPages(int page, int pageSize);
        List<BookingDTO> GetActivesByPages(int page, int pageSize);
        decimal GetTotalPrice(BookingDTO bookingDto);
        string GetRoute(BookingDTO bookingDto);
        int CreateBooking(BookingDTO bookingDto);

    }
}