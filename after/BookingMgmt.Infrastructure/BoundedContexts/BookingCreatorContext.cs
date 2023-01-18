using System.Data.Entity;
using BookingMgmt.Domain.Entities;
using BookingMgmt.SharedKernel.DbContext;
using Microsoft.Extensions.Configuration;

namespace BookingMgmt.Infrastructure.BoundedContexts
{
    public class BookingCreatorContext : DbContextBase
    {
        static BookingCreatorContext()
        {
            Database.SetInitializer<BookingCreatorContext>(null);
        }

        public BookingCreatorContext(IConfiguration configuration)
            : base(configuration["DatabaseConnectionString"])
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }

    }
}
