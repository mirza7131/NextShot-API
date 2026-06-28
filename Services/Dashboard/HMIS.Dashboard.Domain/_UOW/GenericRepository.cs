using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Repositories.UOW
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetALL(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;



            //// first we get the property of the model.
            //var property =  dbSet.GetType();
            // var pro =    property.GetProperty("ActionTypeId");


            //// lets assume the property exists and is a nullable bool; get the value from the property.
            //var propertyValue = (bool?)pro.GetValue(dbSet);

            //// now check if the propertyValue not has a value.
            //if (!propertyValue.HasValue)
            //{
            //    // set the value
            //    query= query.Where(x => typeof(TEntity) ?.GetProperty("ActionTypeId"). != (int)ActionTypeEnum.Deleted);
            //}


            //query = query.Where(x => (int)typeof(TEntity).GetProperty("ActionTypeId").GetValue("ActionTypeId") != (int)ActionTypeEnum.Deleted);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                return dbSet.Count();
            }
            return dbSet.Count(filter);
        }

        public virtual double GetSum(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, double>> sum)
        {
            if (filter == null)
            {
                return dbSet.Sum(sum);
            }
            return dbSet.Where(filter).Sum(sum);
        }

        public virtual async Task<TEntity?> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity?> GetByCnic(object cnic)
        {
            return await dbSet.FindAsync(cnic);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return await Task.FromResult(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }


    }
}
