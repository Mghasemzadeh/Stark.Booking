using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using EGS.Stark.Web;

namespace EGS.Stark.Booking.HttpService.Helper
{
    public class SortBuilder
    {
        public static IQueryable<TEntity> SortQueries<TEntity>(IList<SortParameter> sortItems, IQueryable<TEntity> data)
        {
            var result = data;
            foreach (var item in sortItems)
            {
                var columnName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.ColumnName.ToLower());
                var param = Expression.Parameter(typeof(TEntity), "p");
                var propertyReference = Expression.Convert(Expression.Property(param, typeof(TEntity).GetProperty(columnName)), typeof(object));
                var expression = Expression.Lambda<Func<TEntity, object>>(propertyReference, new[] { param }).Compile();

                var propertyInfo = typeof(TEntity).GetProperty(item.ColumnName);
                if (item.Type == SortTypes.Ascending)
                    result = result.OrderBy(expression).AsQueryable();
                else
                    result = result.OrderByDescending(expression).AsQueryable();
            }
            return result;
        }
    }
}