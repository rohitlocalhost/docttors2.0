namespace Docttors_portal.DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetEnumValue<T>(int intValue);
        void Add(T entity);
        void AddAll(IEnumerable<T> entity);
        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entity);
        void Update(T entity);
        bool Any();
        T GetSingle(Expression<Func<T, bool>> whereCondition);
        ///// <summary>
        ///// Gets the object set.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //IObjectSet<T> GetObjectSet<T>() where T : class;
        /// <summary>
        /// Pageds the list.
        /// </summary>
        /// <param name="noofRecords">The noof records.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="noofPages">The noof pages.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        IList<T> PagedList(out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int64 noofPages, Func<T, IComparable> orderBy);
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
        IList<T> PagedList(Expression<Func<T, bool>> whereCondition, out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy);
        /// <summary>
        /// Pageds the list.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="noofRecords">The noof records.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="noofPages">The noof pages.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IList<T> PagedList(Expression<Func<T, bool>> whereCondition, out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy, string sortOrder);
        /// <summary>
        /// Get the object list.
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        List<T> GetAll(Expression<Func<T, bool>> whereCondition);
        IList<T> PagedListWithInt32(out Int64 noofRecords, Int32 pageNo, Int32 pageSize, out Int32 noofPages, Func<T, IComparable> orderBy);
    }
}
