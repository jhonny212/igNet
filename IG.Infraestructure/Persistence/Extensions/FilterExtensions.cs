using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IG.Infraestructure.Persistence.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> WhereSearch<T>(
            this IQueryable<T> query,
            string? searchTerm,
            params Expression<Func<T, string>>[] columns
        )
            where T : class
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || columns.Length == 0)
                return query;

            searchTerm = searchTerm.ToLower();

            Expression<Func<T, bool>>? predicate = null;

            foreach (var column in columns)
            {
                var parameter = column.Parameters[0];

                var body = Expression.Call(
                    Expression.Call(column.Body, nameof(string.ToLower), null),
                    nameof(string.Contains),
                    null,
                    Expression.Constant(searchTerm)
                );

                var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

                predicate = predicate == null ? lambda : OrElse(predicate, lambda);
            }

            return query.Where(predicate!);
        }

        private static Expression<Func<T, bool>> OrElse<T>(
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2
        )
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.OrElse(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
