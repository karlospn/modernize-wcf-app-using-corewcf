using BookingMgmt.CoreWCF.WebService.DTO;

namespace BookingMgmt.CoreWCF.WebService
{
    [ServiceContract]
    public interface IBookingCreatorService
    {
        [OperationContract]
        int CreateBooking(BookingRequest booking);

        [OperationContract]
        void CancelBooking(int bookingId);

        [OperationContract]
        List<BookingResponse> GetCanceledBookings(int page, int pageSize);

        [OperationContract]
        List<BookingResponse> GetActiveBookings(int page, int pageSize);

    }
}