using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookingMgmt.SharedKernel.UnitOfWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected System.Data.Entity.DbContext Context;
        protected DbSet<TEntity> _DbSet;

        public Repository(System.Data.Entity.DbContext context)
        {
            this.Context = context;
            this._DbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity entity) => this._DbSet.Add(entity);

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                this._DbSet.AddRange(entities);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => this._DbSet.Where<TEntity>(predicate);

        public TEntity FindById(object id) => this._DbSet.Find(new object[1]
        {
      id
        });

        public virtual TEntity FirstOrDefault(
          Expression<Func<TEntity, bool>> predicate,
          bool trackingEnabled = false)
        {
            return (trackingEnabled ? (IQueryable<TEntity>)this._DbSet : (IQueryable<TEntity>)this._DbSet.AsNoTracking()).FirstOrDefault<TEntity>(predicate);
        }

        public virtual IQueryable<TEntity> GetAll() => (IQueryable<TEntity>)this._DbSet;

        public virtual IQueryable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includeProperties = null,
          int? page = null,
          int? pageSize = null,
          bool trackingEnabled = false)
        {
            IQueryable<TEntity> query = (IQueryable<TEntity>)this._DbSet;
            includeProperties?.ForEach((Action<Expression<Func<TEntity, object>>>)(i => query = query.Include<TEntity, object>(i)));
            if (filter != null)
                query = query.Where<TEntity>(filter);
            if (orderBy != null)
                query = (IQueryable<TEntity>)orderBy(query);
            if (page.HasValue && pageSize.HasValue)
                query = query.Skip<TEntity>((page.Value - 1) * pageSize.Value).Take<TEntity>(pageSize.Value);
            return trackingEnabled ? query : query.AsNoTracking<TEntity>();
        }

        public void UpdateRootEntity(TEntity entity)
        {
            if ((object)entity == null || this.Context.Entry<TEntity>(entity).State != EntityState.Detached)
                return;
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void UpdateManyRootEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    this.UpdateRootEntity(entity);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }

        public virtual void UpdateGraph(TEntity entity) => throw new NotImplementedException();

        public virtual void UpdateGraphs(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            try
            {
                foreach (TEntity entity in entities)
                    this.UpdateGraph(entity);
            }
            finally
            {
            }
        }

        public void Delete(object id)
        {
            TEntity entity = this._DbSet.Find(new object[1] { id });
            if ((object)entity == null)
                return;
            this.Delete(entity);
        }

        public void DeleteMany(IEnumerable<object> ids)
        {
            try
            {
                List<TEntity> entityList = new List<TEntity>();
                this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (object id in ids)
                {
                    TEntity entity = this._DbSet.Find(new object[1]
                    {
            id
                    });
                    if ((object)entity != null)
                        entityList.Add(entity);
                }
                this.DeleteMany((IEnumerable<TEntity>)entityList);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void Delete(TEntity entity)
        {
            if ((object)entity == null)
                return;
            this.Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            bool detectChangesEnabled = this.Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                if (entities == null)
                    return;
                if (detectChangesEnabled)
                    this.Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                    this.Delete(entity);
            }
            finally
            {
                this.Context.Configuration.AutoDetectChangesEnabled = detectChangesEnabled;
            }
        }
    }
}
