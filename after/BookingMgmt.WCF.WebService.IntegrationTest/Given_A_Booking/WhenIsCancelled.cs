using System;
using System.Collections.Generic;
using BookingMgmt.Contracts;
using BookingMgmt.Contracts.DTO;
using BookingMgmt.WCF.WebService.IntegrationTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingMgmt.WCF.WebService.IntegrationTest.Given_A_Booking
{
    [TestClass]
    public class WhenIsCancelled
    {

        private static BookingDTO _bookingRequest;
        private int _id;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            
            _bookingRequest = new BookingDTO
            {
                SalesAgent = "corp",
                Journeys = new List<JourneyDTO>
                {
                    new JourneyDTO()
                    {
                        Departure = "BCN",
                        Arrival = "MAD",
                        Price = 150,
                        DepartureDate = DateTime.UtcNow,
                        ArrivalDate = DateTime.UtcNow.AddHours(1)
                    },
                    new JourneyDTO
                    {
                        Departure = "MAD",
                        Arrival = "BIO",
                        Price = 110,
                        DepartureDate = DateTime.UtcNow.AddHours(2),
                        ArrivalDate = DateTime.UtcNow.AddHours(3)
                    }
                }
            };
        }


        [TestInitialize]
        public void TestInitialize()
        {
            ServiceLocator.Build();
            var service = ServiceLocator.Resolve<IBookingCreatorApplicationServices>();
            _id = service.CreateBooking(_bookingRequest);
        }


        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("BookingMgmt.WCF.WebService")]
        public void Then_Returns_Success()
        {
            try
            {
                var service = ServiceLocator.Resolve<IBookingCreatorApplicationServices>();
                service.CancelBooking(_id);
            }
            catch (Exception)
            {
                Assert.Fail("Expected exception not to be thrown");
                throw;
            }
        }


        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("BookingMgmg.WCF.WebService")]
        public void Then_Calls_GetCancelledBookings_successfully()
        {
            try
            {
                var service = ServiceLocator.Resolve<IBookingCreatorApplicationServices>();
                var items = service.GetCanceledByPages(1,1);
                Assert.AreEqual(items.Count, 1);
            }
            catch (Exception)
            {
                Assert.Fail("Expected exception not to be thrown");
                throw;
            }
        }


    }
}
