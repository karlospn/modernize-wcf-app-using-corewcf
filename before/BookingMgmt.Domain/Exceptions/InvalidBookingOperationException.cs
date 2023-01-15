using System;

namespace BookingMgmt.Domain.Exceptions
{
    [Serializable]
    public class InvalidBookingOperationException : Exception
    {

        public InvalidBookingOperationException()
        {

        }
        public InvalidBookingOperationException(string message) : base(message)
        {

        }
        public InvalidBookingOperationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected InvalidBookingOperationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
