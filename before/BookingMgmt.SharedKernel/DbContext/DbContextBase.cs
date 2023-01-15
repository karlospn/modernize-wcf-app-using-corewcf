using System.Data.Entity;

namespace BookingMgmt.SharedKernel.DbContext
{
    public class DbContextBase : System.Data.Entity.DbContext
    {
        public DbContextBase(string databaseConnectionString)
            : base(databaseConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

    }
}
