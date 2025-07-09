using Docttors_portal.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Docttors_portal.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private readonly IDbSet<T> _dbset;

        public BaseRepository(IDbContext context)
        {
            if (context != null)
            {
                _context = context;
                _dbset = context.Set<T>();
            }
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public T GetEnumValue<T>(int intValue)
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }


        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            try
            {
                _dbset.Add(entity);
                _context.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var ex in e.EntityValidationErrors)
                {
                    foreach (var item in ex.ValidationErrors)
                    {
                        throw;
                    }
                }
            }



        }

        /// <summary>
        /// Add All
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddAll(IEnumerable<T> entity)
        {
            if (entity != null)
            {
                foreach (var ent in entity)
                {
                    _dbset.Add(ent);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _context.Entry(entity);
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete All
        /// </summary>
        /// <param name="entity"></param>
        public virtual void DeleteAll(IEnumerable<T> entity)
        {
            if (entity != null)
            {
                foreach (var ent in entity)
                {
                    _context.Entry(ent);
                    _dbset.Remove(ent);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get Single
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return _context.Set<T>().Where(whereCondition).FirstOrDefault<T>();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
        /// <summary>
        /// Delete Statement
        /// </summary>
        /// <param name="entity"></param>
        //public virtual void DeleteStatement(T entity)
        //{
        //    var entry = _context.Entry(entity);
        //    _dbset.Attach(entity);
        //    entry.State = System.Data.EntityState.Deleted;
        //    _context.SaveChanges();
        //}
        /// <summary>
        /// Get Any
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return _dbset.Any();
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return _dbset.LongCount<T>();
        }

        /// <summary>
        /// Counts the specified where condition.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> whereCondition)
        {
            return _dbset.Where(whereCondition).LongCount<T>();
        }

        /// <summary>
        /// Pageds the list.
        /// </summary>
        /// <param name="noofRecords">The noof records.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="noofPages">The noof pages.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid Page number;original</exception>
        public IList<T> PagedList(out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int64 noofPages, Func<T, IComparable> orderBy)
        {
            noofRecords = Count();
            noofPages = (Int32)(noofRecords / pageSize);
            if (noofRecords % pageSize > 0)
                noofPages++;
            if (pageNo > noofPages)
                throw new System.ArgumentException("Invalid Page number", "original");
            var products = _dbset.OrderBy(orderBy).AsQueryable();
            return products.Skip(pageSize * pageNo).Take(pageSize).ToList();
        }

        /// <summary>
        /// Pageds the list.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="noofRecords">The noof records.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="noofPages">The noof pages.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public IList<T> PagedList(Expression<Func<T, bool>> whereCondition, out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy)
        {
            noofRecords = Count(whereCondition);
            noofPages = (Int32)(noofRecords / pageSize);
            if (noofRecords % pageSize > 0)
                noofPages++;
            if (pageNo > noofPages)
                return null;
            if (pageNo == noofPages)
                pageNo = pageNo - 1;
            return _dbset.Where(whereCondition).OrderBy(orderBy).Skip(pageSize * pageNo).Take(pageSize).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition">where condition</param>
        /// <param name="noofRecords">no of total records based on query</param>
        /// <param name="pageNo">current page no</param>
        /// <param name="pageSize">no of items per page</param>
        /// <param name="noofPages">total page in list</param>
        /// <param name="orderBy">order by query</param>
        /// <param name="sortOrder">asc or desc</param>
        /// <returns></returns>
        public IList<T> PagedList(Expression<Func<T, bool>> whereCondition, out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy, string sortOrder)
        {
            noofRecords = Count(whereCondition);
            noofPages = (Int32)(noofRecords / pageSize);
            if (noofRecords % pageSize > 0)
                noofPages++;
            if (pageNo > noofPages)
                return null;
            if (pageNo == noofPages)
                pageNo = pageNo - 1;
            if (sortOrder == "asc")
                return _dbset.Where(whereCondition).OrderBy(orderBy).Skip(pageSize * pageNo).Take(pageSize).ToList();
            else
                return _dbset.Where(whereCondition).OrderByDescending(orderBy).Skip(pageSize * pageNo).Take(pageSize).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition">where condition</param>
        /// <returns></returns>
        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _dbset.Where(whereCondition).ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noofRecords">no of total records based on query</param>
        /// <param name="pageNo">current page no</param>
        /// <param name="pageSize">no of items per page</param>
        /// <param name="noofPages">total page in list</param>
        /// <param name="orderBy">order by query</param>
        /// <returns></returns>
        public IList<T> PagedListWithInt32(out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy)
        {
            noofRecords = Count();
            noofPages = (Int32)(noofRecords / pageSize);
            if (noofRecords % pageSize > 0)
                noofPages++;
            if (pageNo > noofPages)
                throw new System.ArgumentException("Invalid Page number", "original");
            var products = _dbset.OrderBy(orderBy).AsQueryable();
            return products.Skip(pageSize * pageNo).Take(pageSize).ToList();
        }
    }
}
