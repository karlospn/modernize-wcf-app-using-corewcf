namespace BookingMgmt.SharedKernel.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Dispose();
        int Save();
        void Dispose(bool disposing);
        IRepository<T> GetRepository<T>() where T : class;
    }
}