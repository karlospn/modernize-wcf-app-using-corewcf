using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookingMgmt.SharedKernel.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<object> ids);

        void DeleteMany(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity FindById(object id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool trackingEnabled = false);

        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null,
            bool trackingEnabled = false);

        IQueryable<TEntity> GetAll();

        void Insert(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        void UpdateGraph(TEntity entity);

        void UpdateGraphs(IEnumerable<TEntity> entities);

        void UpdateManyRootEntities(IEnumerable<TEntity> entities);

        void UpdateRootEntity(TEntity entity);
    }
}