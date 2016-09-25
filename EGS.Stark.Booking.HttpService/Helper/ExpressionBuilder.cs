using EGS.Stark.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EGS.Stark.Booking.HttpService.Helper
{
    public class ExpressionBuilder
    {
        public static Func<TEntity, bool> And<TEntity>(ApiParameters parameters)
        {
            Expression expression = null;
            var param = Expression.Parameter(typeof(TEntity), "p");
            FilteringObject property = FilteredMapping(parameters);

            if (property.UserId != 0)
            {
                expression = Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("UserId")), Expression.Constant(property.UserId));
            }
            if (property.MerchantId != 0)
            {
                if (expression == null)
                    expression = Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("MerchantId")), Expression.Constant(property.MerchantId));
                else
                    expression = Expression.AndAlso(expression, Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("MerchantId")), Expression.Constant(property.MerchantId)));
            }
            if (property.PurposeId != 0)
            {
                if (expression == null)
                    expression = Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("PurposeId")), Expression.Constant(property.PurposeId));
                else
                    expression = Expression.AndAlso(expression, Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("PurposeId")), Expression.Constant(property.PurposeId)));
            }
            if (property.StatusId != 0)
            {
                if (expression == null)
                    expression = Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("StatusId")), Expression.Constant(property.StatusId));
                else
                    expression = Expression.AndAlso(expression, Expression.Equal(Expression.Property(param, typeof(TEntity).GetProperty("StatusId")), Expression.Constant(property.StatusId)));
            }


            var compiledExp = expression != null ? Expression.Lambda<Func<TEntity, bool>>(expression, new[] { param }).Compile() : null;

            return compiledExp;
        }


        public static FilteringObject FilteredMapping(ApiParameters parameters)
        {
            FilteringObject param = new FilteringObject();

            return param;
        }
    }

}