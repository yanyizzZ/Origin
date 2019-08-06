using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OriginAdminWeb
{
    public static class EFHelper
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, string sortDirection)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (sortDirection == "asc") ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

    }
}