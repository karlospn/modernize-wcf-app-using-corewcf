using System.Configuration;
using System.Data.Entity;
using BookingMgmt.Domain.Entities;
using BookingMgmt.SharedKernel.DbContext;

namespace BookingMgmt.Infrastructure.BoundedContexts
{
    public class BookingCreatorContext : DbContextBase
    {
        static BookingCreatorContext()
        {
            Database.SetInitializer<BookingCreatorContext>(null);
        }

        public BookingCreatorContext()
            : base(ConfigurationManager.AppSettings["DatabaseConnectionString"])
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }

    }
}
