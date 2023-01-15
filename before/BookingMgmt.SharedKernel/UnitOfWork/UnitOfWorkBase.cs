using System;
using System.Collections;

namespace BookingMgmt.SharedKernel.UnitOfWork
{
    public abstract class UnitOfWorkBase
    {
        private readonly System.Data.Entity.DbContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWorkBase(System.Data.Entity.DbContext context) => this._context = context;

        public int Save() => this._context.SaveChanges();

        public virtual IRepository<T> GetRepository<T>() where T : class => (IRepository<T>)this.GetRepository<T>(typeof(Repository<>));

        protected object GetRepository<T>(Type repositoryType) where T : class
        {
            if (this._repositories == null)
                this._repositories = new Hashtable();
            string name = typeof(T).Name;
            if (!this._repositories.ContainsKey((object)name))
            {
                object newInstance = this.GetNewInstance<T>(repositoryType);
                this._repositories.Add((object)name, newInstance);
            }
            return this._repositories[(object)name];
        }

        private object GetNewInstance<T>(Type repositoryType) where T : class => repositoryType.IsGenericTypeDefinition ? Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), (object)this._context) : Activator.CreateInstance(repositoryType, (object)this._context);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
                this._context.Dispose();
            this._disposed = true;
        }
    }
}
