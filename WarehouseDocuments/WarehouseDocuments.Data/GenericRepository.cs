using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Data
{
    public class GenericRepository<TEntity> where TEntity : class, IEntity
    {
        private WarehouseDocumetDbContext db = null;
        private DbSet<TEntity> dbSet;

        public GenericRepository(WarehouseDocumetDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> List(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           IEnumerable<string> includes = null )
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var item in includes)
                {
                  query = query.Include(item);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, IEnumerable<string> includes = null)
        {
            DbQuery<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (var item in includes)
                {
                  query = query.Include(item);
                }
            }

            return dbSet.FirstOrDefault(filter);
        }
        public virtual int Insert(TEntity entity)
        {
            var res = dbSet.Add(entity);
            return res.Id;
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }


    }
}
