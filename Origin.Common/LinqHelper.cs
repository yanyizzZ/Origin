using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Origin.Common
{
    /// <summary>
    /// 排序
    /// </summary>
   public class LinqHelper
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IQueryable<T> DataSorting<T>(IQueryable<T> source, string sortExpression, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.ToUpper().Trim() == "ASC")
                sortingDir = "OrderBy";
            else if (sortDirection.ToUpper().Trim() == "DESC")
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);
            PropertyInfo pi = typeof(T).GetProperty(sortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> DataPaging<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 1)
            {
                return source.Take(pageSize);
            }
            else
            {
                return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
        }
        /// <summary>
        /// 排序并分页 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> SortingAndPaging<T>(IQueryable<T> source, string sortExpression, string sortDirection, int pageNumber, int pageSize)
        {
            IQueryable<T> query = DataSorting<T>(source, sortExpression, sortDirection);
            return DataPaging(query, pageNumber, pageSize);
        }
        #region
//       可参考我的解决方法，使用DynamicLinq，支持条件过滤，分页，排序，最关键的是还能序列化，这在N-Tier下查询是很有用的
//http://www.cnblogs.com/gyche/p/3223058.html
        //public IQueryable<T> GeneralSortMethod<T>(IQueryable<T> data, params FiledOrderParam[] orderParams) where T : class
        //{
        //    //Compose the expression tree that represents the parameter to the predicate. ie. p=>
        //    var parameter = Expression.Parameter(typeof(T), "p");

        //    if (orderParams != null && orderParams.Length > 0)
        //    {
        //        for (int i = 0; i < orderParams.Length; i++)
        //        {
        //            //Get property of chosen field. 
        //            var property = typeof(T).GetProperty(orderParams[i].PropertyName);
        //            //
        //            if (property != null)
        //            {
        //                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //                //p=>p.Property
        //                var orderByExpr = Expression.Lambda(propertyAccess, parameter);

        //                string methodName = i > 0 ?
        //                    orderParams[i].IsDesc ? "ThenByDescending" : "ThenBy"
        //                    : orderParams[i].IsDesc ? "OrderByDescending" : "OrderBy";

        //                //methodName(p=>p.Property)
        //                var resultExp = Expression.Call(
        //                    typeof(Queryable), methodName,
        //                    new Type[] { typeof(T), property.PropertyType },
        //                    data.Expression, Expression.Quote(orderByExpr)
        //                    );

        //                data = data.Provider.CreateQuery<T>(resultExp);
        //            }
        //        }
        //    }
        //    return data;
        //}

        //public class FiledOrderParam
        //{
        //    public bool IsDesc { get; set; }
        //    public string PropertyName { get; set; }
        //}
        #endregion
    }
}
