using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.Application.Core.Interfaces;

namespace IG.Application.Infraestructure.Persistence.Extensions
{
    public static class QueryablePaginationExtensions
    {
        public static IQueryable<T> ToPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize,
            out int totalRecords,
            out int totalPages
        )
        {
            totalRecords = query.Count();
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page <= 0)
                page = 1;
            if (pageSize <= 0)
                pageSize = totalRecords + 1;
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ToSort<T>(
            this IQueryable<T> query,
            string? sortBy,
            bool ascending = true
        )
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            var entityType = typeof(T);
            var parameter = Expression.Parameter(entityType, "x");
            Expression propertyAccess = parameter;
            foreach (var propertyName in sortBy.Split('.'))
            {
                var property = propertyAccess
                    .Type.GetProperties()
                    .FirstOrDefault(p =>
                        string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase)
                    );
                if (property == null)
                    throw new ArgumentException(
                        $"Property '{sortBy}' not found on type '{entityType.Name}'"
                    );
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            var method = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(entityType, propertyAccess.Type);
            return (IQueryable<T>)method.Invoke(null, new object[] { query, orderByExpression })!;
        }

        public static IQueryable<T> ToSort<T>(this IQueryable<T> query, ISort sort)
        {
            return query.ToSort(sort.SortBy, sort.IsDescending);
        }

        public static IQueryable<T> ToPage<T>(
            this IQueryable<T> query,
            IPaged page,
            out int totalRecords,
            out int totalPages
        )
        {
            return query.ToPaged(page.Page, page.PageSize, out totalRecords, out totalPages);
        }

        public static IQueryable<T> ToPageAndSort<T>(
            this IQueryable<T> query,
            IPagedAndSort pageAndSort,
            out int totalRecords,
            out int totalPages
        )
        {
            var sortedQuery = query.ToSort(pageAndSort);
            return sortedQuery.ToPage(pageAndSort, out totalRecords, out totalPages);
        }
    }
}
